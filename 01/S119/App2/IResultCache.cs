using Shared;

namespace App2
{
public interface IResultCache
{
    Task<Output> GetAsync(string method, Input input);
    Task SaveAsync(string method, Input input, Output output);
    Task ClearAsync(params string[] methods);
}
}
