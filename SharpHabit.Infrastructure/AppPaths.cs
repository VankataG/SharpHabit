namespace SharpHabit.Infrastructure
{
    public static class AppPaths
    {
        public static string StorageFilePath 
        {
            get
            {
                string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string appFolder = Path.Combine(appData, "SharpHabit");

                return Path.Combine(appFolder, "sharphabit.json");
            }
        }
    }
}
