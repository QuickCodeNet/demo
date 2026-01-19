namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class MessageTemplate
    {
        public static class Command
        {
            private const string _prefix = "SmsManagerModule.MessageTemplate.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateContent => ResourceKey("UpdateContent.g.sql");
            public static string UpdateType => ResourceKey("UpdateType.g.sql");
        }
    }
}