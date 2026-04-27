namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Model
    {
        public static class Command
        {
            private const string _prefix = "IdentityModule.Model.Command";
            private const string _sqlScriptStem = "Model";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string DeleteModelsWithModuleName => ResourceKey($"{_sqlScriptStem}.DeleteModelsWithModuleName.g.sql");
        }
    }
}