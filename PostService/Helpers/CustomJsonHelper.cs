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
            return await Task.Run(() =>
             {
                 using StreamReader file = File.OpenText(FilePath);
                 var json = file.ReadToEnd();
                 return JsonConvert.DeserializeObject<IEnumerable<T>>(json) ?? [];
             });
        }

        public async Task Write<T>(IEnumerable<T> data)
        {
            await Task.Run(() =>
           {
               using StreamWriter file = File.CreateText(FilePath);
               JsonSerializer serializer = new JsonSerializer();
               serializer.Serialize(file, data);
           });
        }
    }
}
