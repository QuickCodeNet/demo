namespace QuickCode.Demo.Common.Helpers;

using Bogus;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System.Threading;

public static class TestDataGenerator
{    
    private static readonly Random _random = new();
    private static readonly Dictionary<Type, Func<Faker, object>> _typeGenerators = new();
    private static readonly Dictionary<string[], Func<Faker, string>> _stringGenerators = new()
    {
        [ new[] { "email", "mail" } ] = f => f.Internet.Email(),
        [ new[] { "phone", "tel" } ] = f => f.Phone.PhoneNumber("5##-###-####"),
        [ new[] { "name", "isim" } ] = f => f.Name.FirstName(),
        [ new[] { "surname", "soyad", "lastname" } ] = f => f.Name.LastName(),
        [ new[] { "fullname", "adsoyad" } ] = f => f.Name.FullName(),
        [ new[] { "title" } ] = f => f.Name.JobTitle(),
        [ new[] { "address", "adres" } ] = f => f.Address.FullAddress(),
        [ new[] { "street" } ] = f => f.Address.StreetAddress(),
        [ new[] { "city" } ] = f => f.Address.City(),
        [ new[] { "country" } ] = f => f.Address.Country(),
        [ new[] { "url", "link" } ] = f => f.Internet.Url(),
        [ new[] { "password" } ] = f => f.Internet.Password(10, true),
        [ new[] { "description", "comment", "aciklama" } ] = f => f.Lorem.Sentence(),
        [ new[] { "note", "not" } ] = f => f.Lorem.Paragraph(),
        [ new[] { "code", "kod" } ] = f => f.Random.AlphaNumeric(8),
        [ new[] { "username", "kullanici" } ] = f => f.Internet.UserName(),
        [ new[] { "company" } ] = f => f.Company.CompanyName(),
        [ new[] { "iban" } ] = f => f.Finance.Iban(),
        [ new[] { "currency" } ] = f => f.Finance.Currency().Code,
        [ new[] { "color", "renk" } ] = f => f.Commerce.Color(),
        [ new[] { "product" } ] = f => f.Commerce.ProductName()
    };
    
    private static readonly HashSet<string> _uniqueKeywords = new() { "id", "key", "code", "platenumber", "email", "username" };
    private static long _uniqueCounter = DateTime.UtcNow.Ticks;
    
    static TestDataGenerator()
    {
        InitializeTypeGenerators();
    }
    
    public static T CreateFake<T>(string cultureInfo = "en") where T : class
    {
        try
        {
            var seed = (int)(DateTime.Now.Ticks % int.MaxValue);
            var faker = new Faker<T>(cultureInfo).UseSeed(seed);
            var visited = new HashSet<Type>();
            ConfigureFakerRecursively(faker, typeof(T), 0, visited, cultureInfo);
            return faker.Generate();
        }
        catch (Exception ex)
        {
            return Activator.CreateInstance<T>();
        }
    }

    public static List<T> CreateFakes<T>(string cultureInfo = "en", int count = 3) where T : class
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateFake<T>(cultureInfo))
            .ToList();
    }

    private static void ConfigureFakerRecursively<T>(Faker<T> faker, Type type, int depth, HashSet<Type> visited, string cultureInfo) where T : class
    {
        if (depth > 3 || visited.Contains(type) || type == null)
            return;

        visited.Add(type);

        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.CanWrite && p.SetMethod != null)
            .ToList();

        foreach (var prop in properties)
        {
            try
            {
                ConfigureProperty(faker, prop, depth, visited, cultureInfo);
            }
            catch
            {
                continue;
            }
        }
    }

    private static void ConfigureProperty<T>(Faker<T> faker, PropertyInfo prop, int depth, HashSet<Type> visited, string cultureInfo) where T : class
    {
        var propName = prop.Name;
        var propType = prop.PropertyType;
        if (Nullable.GetUnderlyingType(propType) != null)
        {
            propType = Nullable.GetUnderlyingType(propType);
        }
        
        if (IsComplexType(propType))
        {
            faker.RuleFor(propName, f => CreateNestedObject(propType, depth + 1, visited, cultureInfo));
        }
        else if (IsCollection(propType))
        {
            faker.RuleFor(propName, f => CreateCollection(propType, depth + 1, visited, cultureInfo));
        }
        else
        {
            ConfigurePrimitiveProperty(faker, prop, propName, propType, cultureInfo);
        }
    }

    private static bool IsComplexType(Type type) =>
        type != null && 
        type.IsClass && 
        type != typeof(string) && 
        type != typeof(DateTime) && 
        type != typeof(DateTimeOffset) &&
        !type.IsEnum &&
        !type.IsPrimitive;

    private static bool IsCollection(Type type) =>
        type != null && 
        type != typeof(string) && 
        typeof(IEnumerable).IsAssignableFrom(type);

    private static object CreateNestedObject(Type type, int depth, HashSet<Type> visited, string cultureInfo)
    {
        if (depth > 3 || visited.Contains(type) || type == null) 
            return null;

        try
        {
            var fakerType = typeof(Faker<>).MakeGenericType(type);
            var faker = Activator.CreateInstance(fakerType, cultureInfo);

            var configureMethod = typeof(TestDataGenerator)
                .GetMethod(nameof(ConfigureFakerRecursively), BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(type);

            configureMethod.Invoke(null, new object[] { faker, type, depth, visited, cultureInfo });

            var generateMethod = fakerType.GetMethod("Generate");
            return generateMethod.Invoke(faker, null);
        }
        catch
        {
            return Activator.CreateInstance(type);
        }
    }

    private static object CreateCollection(Type type, int depth, HashSet<Type> visited, string cultureInfo)
    {
        if (depth > 3 || type == null) return null;

        try
        {
            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var list = CreateListOfType(elementType, depth, visited, cultureInfo);
                return ((IList)list).Cast<object>().ToArray();
            }
            else if (type.IsGenericType)
            {
                var elementType = type.GetGenericArguments()[0];
                return CreateListOfType(elementType, depth, visited, cultureInfo);
            }
        }
        catch { }

        return null;
    }

    private static object CreateListOfType(Type elementType, int depth, HashSet<Type> visited, string cultureInfo)
    {
        if (elementType == null) return null;

        var listType = typeof(List<>).MakeGenericType(elementType);
        var list = (IList)Activator.CreateInstance(listType);

        var count = _random.Next(1, 4);
        for (int i = 0; i < count; i++)
        {
            var value = IsComplexType(elementType)
                ? CreateNestedObject(elementType, depth + 1, visited, cultureInfo)
                : CreatePrimitiveValue(elementType, cultureInfo);

            if (value != null)
                list.Add(value);
        }

        return list;
    }

    private static void ConfigurePrimitiveProperty<T>(Faker<T> faker, PropertyInfo prop, string propName, Type propType, string cultureInfo) where T : class
    {
        if (propType == typeof(string))
        {
            var fakeStringGenerator = GetMockStringGenerator(propName);
            faker.RuleFor(propName, fakeStringGenerator);
            return;
        }
        
        if (_typeGenerators.TryGetValue(propType, out var generator))
        {
            faker.RuleFor(propName, f => generator(f));
            return;
        }

        if (propType.IsEnum)
        {
            faker.RuleFor(propName, f => f.PickRandom(Enum.GetValues(propType).Cast<object>()));
        }
        else
        {
            faker.RuleFor(propName, f => CreatePrimitiveValue(propType, cultureInfo));
        }
    }

    private static Func<Faker, string> GetMockStringGenerator(string propName)
    {
        var lowerName = propName.ToLowerInvariant();

        if (_uniqueKeywords.Any(keyword => lowerName.Contains(keyword)))
        {
            return f => $"{lowerName}_{Interlocked.Increment(ref _uniqueCounter)}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";
        }

        foreach (var entry in _stringGenerators)
        {
            if (entry.Key.Any(keyword => lowerName.Contains(keyword)))
                return entry.Value;
        }
        
        return f => $"{f.Random.Word()}_{Interlocked.Increment(ref _uniqueCounter)}_{Guid.NewGuid().ToString("N").Substring(0, 8)}";
    }

    private static void InitializeTypeGenerators()
    {
        _typeGenerators[typeof(string)] = f => GenerateFakeStringValue(f, "default");
        _typeGenerators[typeof(int)] = f => (int)Interlocked.Increment(ref _uniqueCounter);
        _typeGenerators[typeof(long)] = f => Interlocked.Increment(ref _uniqueCounter);
        _typeGenerators[typeof(decimal)] = f => (decimal)Interlocked.Increment(ref _uniqueCounter);
        _typeGenerators[typeof(double)] = f => (double)Interlocked.Increment(ref _uniqueCounter);
        _typeGenerators[typeof(float)] = f => (float)Interlocked.Increment(ref _uniqueCounter);
        _typeGenerators[typeof(DateTime)] = f => f.Date.Recent();
        _typeGenerators[typeof(DateTime?)] = f => f.Date.Recent();
        _typeGenerators[typeof(DateTimeOffset)] = f => new DateTimeOffset(f.Date.Recent());
        _typeGenerators[typeof(DateTimeOffset?)] = f => new DateTimeOffset(f.Date.Recent());
        _typeGenerators[typeof(bool)] = f => f.Random.Bool();
        _typeGenerators[typeof(Guid)] = f => f.Random.Guid();
        
        var baseTypes = new[]
        {
            typeof(int), typeof(long), typeof(decimal), typeof(double),
            typeof(float), typeof(bool), typeof(Guid)
        };

        foreach (var baseType in baseTypes)
        {
            var nullableType = typeof(Nullable<>).MakeGenericType(baseType);
            _typeGenerators[nullableType] = f => _typeGenerators[baseType](f);
        }
    }

    private static string GenerateFakeStringValue(Faker faker, string propName)
    {
        var name = propName.ToLowerInvariant();

        foreach (var generator in _stringGenerators)
        {
            if (generator.Key.Any(keyword => name.Contains(keyword)))
                return generator.Value(faker);
        }

        return faker.Random.Word();
    }

    private static object CreatePrimitiveValue(Type type, string cultureInfo)
    {
        if (type == null) return null;

        try
        {
            if (_typeGenerators.TryGetValue(type, out var generator))
            {
                var faker = new Faker(cultureInfo);
                return generator(faker);
            }

            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                return values.Length > 0 ? values.GetValue(_random.Next(values.Length)) : null;
            }

            if (type == typeof(string)) return "Test";
            if (type == typeof(int)) return Interlocked.Increment(ref _uniqueCounter);
            if (type == typeof(long)) return Interlocked.Increment(ref _uniqueCounter);
            if (type == typeof(decimal)) return Convert.ToDecimal(Interlocked.Increment(ref _uniqueCounter));
            if (type == typeof(double)) return Interlocked.Increment(ref _uniqueCounter);
            if (type == typeof(float)) return (float)Interlocked.Increment(ref _uniqueCounter);
            if (type == typeof(DateTime)) return DateTime.Now.AddDays(-_random.Next(1, 100));
            if (type == typeof(DateTimeOffset)) return DateTimeOffset.Now.AddDays(-_random.Next(1, 100));
            if (type == typeof(DateTimeOffset?)) return DateTimeOffset.Now.AddDays(-_random.Next(1, 100));
            if (type == typeof(bool)) return _random.Next(2) == 1;
            if (type == typeof(Guid)) return Guid.NewGuid();

            return Activator.CreateInstance(type);
        }
        catch
        {
            return null;
        }
    }
    
    public static void AssertPropertiesEqual<T>(this T expected, T actual)
    {
        foreach (var property in typeof(T).GetProperties())
        {
            var expectedValue = property.GetValue(expected);
            var actualValue = property.GetValue(actual);
            Assert.Equal(expectedValue, actualValue);
        }
    }
    
    public static T Clone<T>(this T source) where T : class
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
    
        var method = source.GetType().GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic);
        return (T)method.Invoke(source, null);
    }
    

    public static Dictionary<string, object> GenerateRandomUpdates<T>(
        this T obj, 
        List<string> ignoredProperties,
        int numberOfFields = 3, 
        string cultureInfo = "en") where T : class
    {
        var updates = new Dictionary<string, object>();
        var properties = typeof(T).GetProperties()
            .Where(p => p.CanWrite && !ignoredProperties.Contains(p.Name))
            .OrderBy(_ => Guid.NewGuid())
            .Take(numberOfFields);

        var fakeData = CreateFake<T>();

        foreach (var property in properties)
        {
            updates[property.Name] = property.GetValue(fakeData)!;
        }

        return updates;
    }
    public static T UpdateProperties<T>(this T original, Dictionary<string, object> updates) where T : class
    {
        var updatedObject = original.Clone();
        var type = typeof(T);

        foreach (var update in updates)
        {
            var property = type.GetProperty(update.Key);
            if (property != null && property.CanWrite)
            {
                property.SetValue(updatedObject, update.Value);
            }
        }

        return updatedObject;
    }
}