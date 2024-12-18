using Newtonsoft.Json;
using PostService.Interfaces;
using System.Reflection.PortableExecutable;

namespace PostService.Helpers
{
    public class CustomJsonHelper : ICustomJsonHelper
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "posts.json");
        public async Task<IEnumerable<T>> Read<T>()
        {
            using StreamReader file = File.OpenText(FilePath);
            var json = await file.ReadToEndAsync();
            return JsonConvert.DeserializeObject<IEnumerable<T>>(json) ?? [];
        }

        public async Task Write<T>(IEnumerable<T> data)
        {
            using StreamWriter file = new(FilePath, false);
            JsonSerializer serializer = new();
            await Task.Yield(); // Yield to avoid blocking the thread
            serializer.Serialize(file, data);
        }
    }
}
