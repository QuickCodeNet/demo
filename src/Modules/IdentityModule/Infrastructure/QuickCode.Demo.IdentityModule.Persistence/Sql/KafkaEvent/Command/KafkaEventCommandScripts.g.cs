namespace QuickCode.Demo.IdentityModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class KafkaEvent
    {
        public static class Command
        {
            private const string _prefix = "IdentityModule.KafkaEvent.Command";
            private const string _sqlScriptStem = "KafkaEvent";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string CleanKafkaEventsWithModuleName => ResourceKey($"{_sqlScriptStem}.CleanKafkaEventsWithModuleName.g.sql");
            public static string CleanKafkaEventsWithModelName => ResourceKey($"{_sqlScriptStem}.CleanKafkaEventsWithModelName.g.sql");
        }
    }
}