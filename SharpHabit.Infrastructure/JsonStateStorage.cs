using System.Text.Json;
using SharpHabit.Core.Storage;

namespace SharpHabit.Infrastructure
{
    public class JsonStateStorage
    {
        public void Save(TrackerState state)
        {
            string path = AppPaths.StorageFilePath;

            string? folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };

            string json = JsonSerializer.Serialize(state, options);
             File.WriteAllText(path, json);
        }

        public TrackerState? Load()
        {
            string path = AppPaths.StorageFilePath;

            if (!File.Exists(path))   return null;

            string json = File.ReadAllText(path);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<TrackerState>(json, options);
        }
    }
}
