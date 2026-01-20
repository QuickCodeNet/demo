namespace QuickCode.Demo.TaskManagerModule.Persistence.Sql;
public static partial class SqlScripts
{
    public static partial class Project
    {
        public static class Command
        {
            private const string _prefix = "TaskManagerModule.Project.Command";
            private static string ResourceKey(string sqlName) => $"{_prefix}.{sqlName}";
            public static string UpdateProjectEndDate => ResourceKey("UpdateProjectEndDate.g.sql");
            public static string DeleteProject => ResourceKey("DeleteProject.g.sql");
        }
    }
}