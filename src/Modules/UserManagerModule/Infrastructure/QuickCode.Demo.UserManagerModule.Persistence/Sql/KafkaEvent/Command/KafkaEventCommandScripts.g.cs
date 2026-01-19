namespace QuickCode.Demo.UserManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class KafkaEvent
    {
        public static class Command
        {
            private const string _prefix = "UserManagerModule.KafkaEvent.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string CleanKafkaEventsWithModuleName => ResourceKey("CleanKafkaEventsWithModuleName.g.sql");
            public static string CleanKafkaEventsWithModelName => ResourceKey("CleanKafkaEventsWithModelName.g.sql");
        }
    }
}