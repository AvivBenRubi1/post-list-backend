namespace PostService.Interfaces
{
    public interface ICustomJsonHelper
    {
        Task<IEnumerable<T>> Read<T>();
        Task Write<T>(IEnumerable<T> data);
    }
}
