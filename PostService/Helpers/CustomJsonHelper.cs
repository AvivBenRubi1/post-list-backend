using Newtonsoft.Json;
using PostService.Interfaces;
using System.Reflection.PortableExecutable;

namespace PostService.Helpers
{
    public class CustomJsonHelper : ICustomJsonHelper
    {
        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "posts.json");

        // Semaphore to limit access to the file
        private static readonly SemaphoreSlim fileSemaphore = new(1, 1);

        public async Task<IEnumerable<T>> Read<T>()
        {
            await fileSemaphore.WaitAsync(); // Wait for the lock

            try
            {
                using StreamReader file = File.OpenText(FilePath);
                var json = await file.ReadToEndAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json) ?? Enumerable.Empty<T>();
            }
            finally
            {
                fileSemaphore.Release(); // Release the lock
            }
        }

        public async Task Write<T>(IEnumerable<T> data)
        {
            await fileSemaphore.WaitAsync(); // Wait for the lock

            try
            {
                using StreamWriter file = new(FilePath, false);
                JsonSerializer serializer = new();
                await Task.Yield(); // Yield to avoid blocking the thread
                serializer.Serialize(file, data);
            }
            finally
            {
                fileSemaphore.Release(); // Release the lock
            }
        }
    }
}
