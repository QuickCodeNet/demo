namespace QuickCode.Demo.SmsManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class OtpType
    {
        public static class Query
        {
            private const string _prefix = "SmsManagerModule.OtpType.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetOtpMessagesForOtpTypes => ResourceKey("GetOtpMessagesForOtpTypes.g.sql");
            public static string GetOtpMessagesForOtpTypesDetails => ResourceKey("GetOtpMessagesForOtpTypesDetails.g.sql");
        }
    }
}