namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Project
    {
        public static class Query
        {
            private const string _prefix = "TaskManagerModule.Project.Query";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string GetProjectsByUserId => ResourceKey("GetProjectsByUserId.g.sql");
            public static string GetProjectsDueSoon => ResourceKey("GetProjectsDueSoon.g.sql");
            public static string SearchProjects => ResourceKey("SearchProjects.g.sql");
        }
    }
}