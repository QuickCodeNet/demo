namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Module
    {
        public static class Query
        {
            private const string _prefix = "IdentityModule.Module.Query";
            private const string _sqlScriptStem = "Module";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string ModuleNameIsExists => ResourceKey($"{_sqlScriptStem}.ModuleNameIsExists.g.sql");
        }
    }
}