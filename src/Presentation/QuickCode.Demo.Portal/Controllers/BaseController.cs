using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using QuickCode.Demo.Portal.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http;
using QuickCode.Demo.Common.Nswag.Clients.UserManagerModuleApi.Contracts;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Caching.Memory;
using QuickCode.Demo.Portal.Models;
using SkiaSharp;
using Humanizer;

namespace QuickCode.Demo.Portal.Controllers
{
    public class BaseController : Controller
    {
        protected const int PageSize = 20;
        protected readonly IMemoryCache Cache;
        protected readonly IHttpContextAccessor HttpContextAccessor;
        protected readonly ITableComboboxSettingsClient TableComboboxSettingsClient;
        
        public BaseController(ITableComboboxSettingsClient tableComboboxSettingsClient, IHttpContextAccessor httpContextAccessor, IMemoryCache cache)
        {
            HttpContextAccessor = httpContextAccessor;
            TableComboboxSettingsClient = tableComboboxSettingsClient;
            Cache = cache;
        }
        
        private string GetAreaName()
        {
            var areaAttribute = (AreaAttribute)Attribute.GetCustomAttribute(GetType(), typeof(AreaAttribute));
            return areaAttribute?.RouteValue ?? "DefaultArea";
        }

        private string GetCacheKey(string suffix = "Data")
        {
            var areaName = GetAreaName();
            var routeSegment = GetRouteSegment();
            return $"{routeSegment.Replace(areaName, string.Empty)}{suffix}";
        }
        
        private string GetRouteSegment()
        {
            var routeAttribute = (RouteAttribute)Attribute.GetCustomAttribute(GetType(), typeof(RouteAttribute));
            var routeTemplate = routeAttribute?.Template ?? string.Empty;
            return routeTemplate.Split('/').LastOrDefault() ?? string.Empty;
        }

        protected void ClearCache()
        {
            var cacheKey = GetCacheKey();
            Cache.Remove(cacheKey);
        }
        
        protected async Task<Dictionary<string, Dictionary<string,string>>> FillComboBoxAsync<T>(string key,
            Func<Task<ObservableCollection<T>>> getDataFunc,
            Func<T, bool> filterPredicate = null)
        {
            var expireMinutes = 5;
            var cacheKey = $"{key}Data"; 
            var comboBoxList = new Dictionary<string, Dictionary<string,string>>();
            
            if (!Cache.TryGetValue(cacheKey, out ObservableCollection<T> cachedData))
            {
                cachedData = await getDataFunc();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(expireMinutes));

                Cache.Set(cacheKey, cachedData, cacheEntryOptions);
            }

            var finalData = filterPredicate != null 
                ? new ObservableCollection<T>(cachedData.Where(filterPredicate))
                : cachedData;
           
            var selectList =  finalData.AsDictionaryList(GetComboBoxList(key));
            
            comboBoxList.Add(key, selectList);
    
            return comboBoxList;
        }

        public string GetErrorDescription(int errorCode)
        {
            return GetErrorDescription(errorCode.ToString());
        }

        public string GetErrorDescription(object errorCode)
        {
            if (errorCode == null)
            {
                return "Success";
            }

            return GetErrorDescription(errorCode.ToString());
        }

        protected ComboBoxFormatData GetComboBoxList(string tableName)
        {
            var returnValue = new ComboBoxFormatData();
            var comboBoxSettings = TableComboboxSettingsClient.TableComboboxSettingsListAsync().Result;
            var tableSetting = comboBoxSettings.FirstOrDefault(i => i.TableName.Pluralize() == tableName);
            returnValue.StringFormat = tableSetting!.StringFormat;
            returnValue.ValueField = tableSetting.IdColumn;
            returnValue.TextFields = tableSetting.TextColumns.Split('|').Select(i => i).ToArray();
            return returnValue;
        }

        public const string ModelBinderModelName = "model";

        private bool IsRedirectToAction
        {
            get
            {
                if (!TempData.Keys.Contains("IsRedirectToAction"))
                {
                    TempData["IsRedirectToAction"] = false;
                }

                return (bool)TempData["IsRedirectToAction"];
            }
            set
            {
                TempData["IsRedirectToAction"] = value;
            }
        }
        
        public override LocalRedirectResult LocalRedirectPermanent(string localUrl)
        {
            IsRedirectToAction = true;
            return base.LocalRedirectPermanent(localUrl);
        }

        public override RedirectResult RedirectPermanent(string url)
        {
            IsRedirectToAction = true;
            return base.RedirectPermanent(url);
        }

        public override RedirectToRouteResult RedirectToRoute(string routeName)
        {
            return base.RedirectToRoute(routeName);
        }
        
        private bool UserIsAuthenticated()
        {
            if (HttpContext.Session.Get<string>("SessionInfo") == null)
            {
                return false;
            }

            return true;
        }
        protected SortedDictionary<DateTime, ActionInfo> actionHistory = null;
        protected SortedDictionary<DateTime, ActionInfo> ActionHistory
        {
            get
            {
                if (actionHistory == null)
                {
                    actionHistory = new SortedDictionary<DateTime, ActionInfo>();
                }

                if (TempData.Get<object>("ActionHistory") == null)
                {
                    TempData.Put("ActionHistory", actionHistory);
                }

                actionHistory = TempData.Get<SortedDictionary<DateTime, ActionInfo>>("ActionHistory");

                return actionHistory;
            }
        }

        protected ActionInfo GetLastAction()
        {
            ActionInfo lastAction = null;
            var actionList = (from A in ActionHistory
                              where A.Value.ControllerName != "Error" && !A.Value.IsRedirectToAction
                              orderby A.Key descending
                              select A);

            if (actionList.Count() > 0)
            {
                lastAction = actionList.First().Value;
            }

            TempData.Put("ActionHistory", ActionHistory);
            return lastAction;
        }

        public class ActionInfo
        {
            public string ActionName { get; set; }
            public string ControllerName { get; set; }
            public string HttpMethod { get; set; }

            public string URL { get; set; }
            public bool IsRedirectToAction { get; set; }
        }

        private static readonly List<string> ExcludedPaths = new List<string>
        {
            "/GetImage",
            "/GetUmlDiagram",
            "/GetUml",
        };
        
        private bool IsExcludedPath(string path)
        {
            return ExcludedPaths.Any(excluded => path.Contains(excluded));
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (Request.Method == "GET" && !IsRedirectToAction && !IsExcludedPath(Request.Path))
            {
                ClearModelBinderData();
            }
            
            IsRedirectToAction = false;

            base.OnActionExecuting(context);
        }

        protected bool IsBackAction()
        {
            return Request.Form["submitAction"] == "Back";
        }

        protected async Task<object> GetItemValue(object serviceClient, string[] parameterKey)
        {
            MethodInfo methodInfo = serviceClient.GetType().GetMethod("GetItemAsync");
            var parameters = new List<object>();
            var methodParameters = methodInfo.GetParameters();
            var parameterAsString = parameterKey[0].Split('|');
            for (int i = 0; i < methodInfo.GetParameters().Length; i++)
            {
                if (methodParameters[i].ParameterType != typeof(CancellationToken))
                {
                    Type t = Nullable.GetUnderlyingType(methodParameters[i].ParameterType) ?? methodParameters[i].ParameterType;
                    object safeValue = (parameterAsString[i] == null) ? null : Convert.ChangeType(parameterAsString[i], t);
                    parameters.Add(safeValue);
                }
                else
                {
                    parameters.Add(null);
                }
            }

            dynamic methodReturnValue = methodInfo.Invoke(serviceClient, parameters.ToArray());
            var item = methodReturnValue.Result;
            var propValue = item.GetType().GetProperty(parameterKey[1]);
            var returnValue = propValue.GetValue(item);

            return await Task.FromResult(returnValue);
        }

        protected async Task<IActionResult> GetImageResult(object serviceClient, string ic)
        {
            var imageValue = await GetItemValue(serviceClient, ic.Split("_"));

            if (imageValue == null)
            {
                string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images"}";

                return base.File(path + "\no_image.png", "image/png");
            }

            return File(imageValue as byte[], "image/png");
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            //İşlem logu atılır tüm actionlar için

            base.OnActionExecuted(context);
        }


        public T GetModel<T>() where T : class
        {
            Type modelType = typeof(T);

            T mainModel = default(T);

            var tempDataValue = TempData.Get<object>(ModelBinderModelName);

            if (tempDataValue != null && modelType == tempDataValue.GetType())
            {
                mainModel = TempData.Get<T>(ModelBinderModelName);
            }

            if (mainModel == null)
            {
                mainModel = Activator.CreateInstance<T>();
            }

            TempData.Put(ModelBinderModelName, mainModel);
            return mainModel;
        }
         

        /// <summary>
        /// remove all controller temp data except the given  key
        /// </summary>
        public void ClearModelBinderData()
        {
            TempData.Remove(ModelBinderModelName);
        }

        public T ModelBinder<T>(ref T requestModel) where T : class
        {
            return ModelBinder<T>(ref requestModel, ModelBinderModelName);
        }

        public T SetModelBinder<T>(ref T requestModel) where T : class
        {
            return SetModelBinder<T>(ref requestModel, ModelBinderModelName);
        }

        public T SetModelBinder<T>(ref T requestModel, string dataKey) where T : class
        {
            TempData.Put(dataKey, requestModel);
            TempData.Keep(dataKey);
            return requestModel;
        }

        public T ModelBinder<T>(ref T requestModel, string dataKey) where T : class
        {
            Type modelType = typeof(T);

            var tempDataValue = TempData.Get<T>(dataKey);

            if (!TempData.Keys.Contains(dataKey) || modelType != tempDataValue.GetType())
            //if (!TempData.Keys.Contains(dataKey) || modelType != tempDataValue.GetType() || Request.ContentType == null)
            {
                TempData.Put(dataKey, requestModel);
            }

            T mainModel = TempData.Get<T>(dataKey);

            if (requestModel != null && Request.ContentType != null)
            {
                foreach (string key in Request.Form.Keys)
                {
                    PropertyInfo pi = modelType.GetProperty(key);
                    if (pi != null)
                    {
                        var propertyValue = pi.GetValue(requestModel, null);
                        pi.SetValue(mainModel, propertyValue, null);
                    }
                    else
                    {
                        var val = GetValue(modelType, key, requestModel);
                        string requestFormKeyValue = null;
                        if (Request.Form.Keys.Contains(key))
                        {
                            requestFormKeyValue = Request.Form[key].AsString().DecodeEndsIsBase64();
                        }
                        
                        if ((val == null && requestFormKeyValue != null) || val.AsString().EndsWith("_IsBase64"))
                        {
                            SetValue(modelType, key, mainModel, requestFormKeyValue);
                        }
                        else
                        {
                            SetValue(modelType, key, mainModel, val);
                        }
                    }
                }

				var getImageExtensionMethodName = "GetImageColumnNames";
                var hasImageProperty = requestModel.GetValueFromProperty("SelectedItem");
                var hasImageColumns = hasImageProperty.HasExtensionMethod(getImageExtensionMethodName);

                if (hasImageColumns)
                {
                    var imageColumnNames = ExtensionInvoker.InvokeExtensionMethod(hasImageProperty, getImageExtensionMethodName) as List<string>;

                    foreach (var imageName in imageColumnNames!)
                    {
                        var key = $"SelectedItem.{imageName}";
                        if (Request.Form.Files[key] != null)
                        {
                            var fileItem = Request.Form.Files[key];

                            var currentValue = GetValue(modelType, key, mainModel);
                            if (currentValue is byte[])
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileItem.CopyTo(memoryStream);
                                    var imageData = memoryStream.ToArray();
                                    //var resizedImageData = CreateThumbnail(1000, 1000, imageData);
                                    if (imageData != null && imageData.Length > 0)
                                    {
                                        SetValue(modelType, key, mainModel, imageData);
                                    }
                                }
                            }
                            //else if (currentValue is string)
                            else
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    fileItem.CopyTo(memoryStream);
                                    var imageData = memoryStream.ToArray();
                                    //var resizedImageData = CreateThumbnail(1000, 1000, imageData);
                                    if (imageData != null && imageData.Length > 0)
                                    {
                                        var fileName = UploadImageAsBlob(fileItem, memoryStream);
                                        SetValue(modelType, key, mainModel, fileName);
                                    }
                                }

                            }
                        }
                        else
                        {
                            var tValue = TempData.Get<T>(dataKey);
                            var lastImageValue = tValue.GetValueFromProperty(key);
                            if(lastImageValue!=null && lastImageValue is String)
                            {
                                SetValue(modelType, key, mainModel, lastImageValue);
                            }
                        }
                    }
                }
            }

            TempData.Put(dataKey, mainModel);
            requestModel = mainModel;
            return mainModel;
        }

		private string UploadImageAsBlob(IFormFile file, MemoryStream memoryStream)
        {
            var accountKey = Environment.GetEnvironmentVariable("AZURE_ACCOUNT_KEY");
            var accountName = Environment.GetEnvironmentVariable("AZURE_ACCOUNT_NAME");
            var blobContainerName = Environment.GetEnvironmentVariable("AZURE_BLOB_CONTAINER_NAME");

            var storageConnectionString = $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net";
            var blobServiceClient = new BlobServiceClient(storageConnectionString);
            var container = blobServiceClient.GetBlobContainerClient(blobContainerName);
            container.CreateIfNotExists();

            var newFileName = $"{Guid.NewGuid()}.{file.ContentType.Split('/').Last()}";
            var blob = container.GetBlobClient(newFileName);

            memoryStream.Position = 0;
            blob.Upload(memoryStream, new BlobHttpHeaders { ContentType = file.ContentType });
            blob.SetAccessTier(AccessTier.Cool);

            return $"{blob.Uri}";
        }

        public byte[] CreateThumbnail(int maxWidth, int maxHeight, byte[] imageData)
        {
            using var inputStream = new MemoryStream(imageData);
            using var original = SKBitmap.Decode(inputStream);

            var ratioX = (double)maxWidth / original.Width;
            var ratioY = (double)maxHeight / original.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(original.Width * ratio);
            var newHeight = (int)(original.Height * ratio);
            
            using var resized = original.Resize(new SKImageInfo(newWidth, newHeight), SKSamplingOptions.Default);
            using var image = SKImage.FromBitmap(resized);
            using var outputStream = new MemoryStream();

            image.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(outputStream);
            return outputStream.ToArray();
        }

        public object GetValue(Type type, object val)
        {
            Type t = Nullable.GetUnderlyingType(type) ?? type;
            if (val != null && typeof(bool) == t && val.ToString() == "true,false")
            {
                return true;
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, val.ToString());
            }

            if (val.GetType() == typeof(string) && t != typeof(string) && val.ToString() == string.Empty)
            {
                return null;
            }

            return (val == null) ? null : Convert.ChangeType(val, t);
        }

        public object GetValue(object value, string propertyName)
        {
            object obj = value;
            if (obj == null)
            {
                return null;
            }

            string[] subPropertyNames = propertyName.Split(new char[] { '.' });

            foreach (string property in subPropertyNames)
            {

            }

            return obj;
        }


        public object ClearBytes(object obj, Type objType)
        {
            if (obj == null && objType != typeof(byte[]))
            {
                return null;
            }

            if (objType == typeof(byte[]))
            {
                if (obj != null && (obj as byte[]).Length>2)
                {
                    return new byte[2];
                }
                else
                {
                    return new byte[1];
                }
                
            }

            var type = obj.GetType();

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                dynamic enumerable = obj;

                if (enumerable != null)
                {
                    var arrayItem = enumerable.ToArray();
                    for (int i = 0; i < arrayItem.Length; i++)
                    {
                        arrayItem[i] = ClearBytes(arrayItem[i], arrayItem[i].GetType());
                    }
                }
            }
            else if ((type.IsClass || type.IsArray) && type != typeof(string) && !type.ToString().Equals("System.Object"))
            {
                if (type.IsArray)
                {
                    var arrayItem = (object[])obj;
                    for (int i = 0; i < arrayItem.Length; i++)
                    {
                        arrayItem[i] = ClearBytes(arrayItem[i], arrayItem[i].GetType());
                    }
                }
                else
                {
                    var properties = type.GetProperties();
                    foreach (var prop in properties)
                    {
                        prop.SetValue(obj, ClearBytes(prop.GetValue(obj), prop.PropertyType));
                    }
                }
            }

            return obj;
        }



        private T[] GetArray<T>(IList<T> iList) where T : new()
        {
            var result = new T[iList.Count];

            iList.CopyTo(result, 0);

            return result;
        }

        private object SetUserDataPasword(Type type,string key,object keyValue)
        {
            if(type.ToString().EndsWith(".UserData") && key == "SelectedItem.Password")
            {
                return keyValue.ToString().EncryptWithHash();
            }

            return keyValue;
        }
        public void SetValue(Type type, string key, object valueObject, object keyValue)
        {
            string[] subItems = key.Split(new char[] { '.' });
            keyValue = SetUserDataPasword(type, key, keyValue);
            if (subItems.Length > 0 && (key.Contains(".") || key.Contains("[")))
            {
                string item = subItems[0];
                if (item.Contains("["))
                {
                    string propertyName = item.Split(new char[] { '[' })[0];
                    int arrayIndex = Convert.ToInt32(item.Replace(propertyName, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty));
                    PropertyInfo pi = type.GetProperty(propertyName);
                    if (pi != null)
                    {
                        object val = null;
                        Type valueType = pi.PropertyType;
                        Array array;
                        if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var arrayValue = pi.GetValue(valueObject, null);
                            MethodInfo miArray = arrayValue.GetType().GetMethod("ToArray");
                            array = (miArray.Invoke(arrayValue, null) as Array);
                        }
                        else
                        {
                            array = (pi.GetValue(valueObject, null) as Array);
                        }


                        val = array.GetValue(arrayIndex);
                        valueType = array.GetType().GetElementType();
                        if (!key.Contains("."))
                        {
                            array.SetValue(GetValue(valueType, keyValue), arrayIndex);
                            pi.SetValue(valueObject, array.ToList(), null);
                        }

                        SetValue(valueType, key.Replace(item + ".", string.Empty), val, keyValue);
                    }
                }
                else
                {
                    string propertyName = item;
                    PropertyInfo pi = type.GetProperty(propertyName);
                    if (pi != null)
                    {
                        object val = null;

                        val = pi.GetValue(valueObject, null);
                        if (val == null)
                        {
                            val = Activator.CreateInstance(pi.PropertyType);
                            pi.SetValue(valueObject, val, null);
                        }
                        object subValue = GetPropValue(keyValue, key.Replace(item + ".", string.Empty));
                        if (subValue == null)
                        {
                            subValue = keyValue;
                        }
                        if (val != null)
                        {
                            SetValue(val.GetType(), key.Replace(item + ".", string.Empty), val, subValue);
                        }
                    }
                }
            }
            else
            {
                PropertyInfo pi = type.GetProperty(key);

                if ( pi != null)
                {
                    var newValue = GetValue(pi.PropertyType, keyValue);
                    if(!(pi.PropertyType == typeof(byte[]) && newValue == null))
                    {
                        pi.SetValue(valueObject, newValue, null);
                    }
                  
                }
            }

        }



        public static object GetPropValue(object obj, string name)
        {
            foreach (string part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }

            return obj;
        }
        public object GetValue(Type type, string key, object valueObject)
        {
            string[] subItems = key.Split(new char[] { '.' });

            if (subItems.Length > 0 && key.Contains("."))
            {
                string item = subItems[0];
                if (item.Contains("["))
                {
                    string propertyName = item.Split(new char[] { '[' })[0];
                    int arrayIndex = Convert.ToInt32(item.Replace(propertyName, string.Empty).Replace("[", string.Empty).Replace("]", string.Empty));
                    PropertyInfo pi = type.GetProperty(propertyName);
                    if (pi != null)
                    {
                        object val = null;
                        if (pi.PropertyType.IsGenericType && pi.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            val = pi.GetValue(valueObject, null);
                            if (val == null)
                            {
                                return null;
                            }

                            IEnumerable<object> array = val as IEnumerable<object>;

                            if (array.Count() > 0)
                            {
                                val = array.ElementAt(arrayIndex);
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            val = (pi.GetValue(valueObject, null) as Array).GetValue(arrayIndex);
                        }

                        return GetValue(val.GetType(), key.Replace(item + ".", string.Empty), val);
                    }
                }
                else
                {
                    PropertyInfo pi = type.GetProperty(item);
                    if (pi != null)
                    {
                        object tempValue = pi.GetValue(valueObject, null);
                        if (tempValue != null)
                        {
                            string newKey = String.Join(".", subItems, 1, subItems.Length - 1);
                            return GetValue(tempValue.GetType(), newKey, tempValue);
                        }
                    }
                }
            }
            else
            {
                PropertyInfo pi = type.GetProperty(key);
                if (pi != null)
                {
                    return pi.GetValue(valueObject, null);
                }
            }

            return null;
        }
    }
}
