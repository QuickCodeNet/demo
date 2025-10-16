using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace QuickCode.Demo.Portal.Helpers;

public static class ExtensionInvoker
{
    private static readonly Dictionary<(Type, string), MethodInfo?> _methodCache = new();

    public static object? InvokeExtensionMethod(object target, string methodName)
    {
        if (target == null || string.IsNullOrWhiteSpace(methodName))
            return null;

        var type = target.GetType();
        var cacheKey = (type, methodName);

        if (!_methodCache.TryGetValue(cacheKey, out var methodInfo))
        {
            methodInfo = FindExtensionMethod(type, methodName);
            _methodCache[cacheKey] = methodInfo;
        }

        return methodInfo?.Invoke(null, new object[] { target });
    }

    private static MethodInfo? FindExtensionMethod(Type targetType, string methodName)
    {
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t != null).ToArray()!;
            }

            foreach (var type in types)
            {
                if (!type.IsSealed || !type.IsAbstract || !type.IsPublic)
                    continue;

                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public |
                                                       BindingFlags.NonPublic))
                {
                    if (method.Name != methodName)
                        continue;

                    if (!method.IsDefined(typeof(System.Runtime.CompilerServices.ExtensionAttribute), false))
                        continue;

                    var parameters = method.GetParameters();
                    if (parameters.Length == 0)
                        continue;

                    if (parameters[0].ParameterType.IsAssignableFrom(targetType))
                        return method;
                }
            }
        }

        return null;
    }

    private static readonly Dictionary<(Type, string), bool> _methodExistenceCache = new();

    public static bool HasExtensionMethod(this object target, string methodName)
    {
        if (target == null || string.IsNullOrWhiteSpace(methodName))
            return false;

        var type = target.GetType();
        var cacheKey = (type, methodName);

        if (_methodExistenceCache.TryGetValue(cacheKey, out var cachedResult))
            return cachedResult;

        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t != null).ToArray()!;
            }

            foreach (var staticClass in types)
            {
                if (!staticClass.IsSealed || !staticClass.IsAbstract || !staticClass.IsPublic)
                    continue;

                foreach (var method in staticClass.GetMethods(BindingFlags.Static | BindingFlags.Public |
                                                              BindingFlags.NonPublic))
                {
                    if (method.Name != methodName)
                        continue;

                    if (!method.IsDefined(typeof(ExtensionAttribute), inherit: false))
                        continue;

                    var parameters = method.GetParameters();
                    if (parameters.Length == 0)
                        continue;

                    if (parameters[0].ParameterType.IsAssignableFrom(type))
                    {
                        _methodExistenceCache[cacheKey] = true;
                        return true;
                    }
                }
            }
        }

        _methodExistenceCache[cacheKey] = false;
        return false;
    }
}