using Dapr.Actors;

namespace Shared
{
    public interface IAccumulator : IActor
    {
        Task<int> IncreaseAsync(int count);
        Task ResetAsync();
    }
}
