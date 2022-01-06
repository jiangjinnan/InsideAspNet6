using Dapr.Actors.Runtime;
using Shared;

namespace App2
{
    public class Accumulator : Actor, IAccumulator
    {
        private readonly string _stateName = "__counter";
        public Accumulator(ActorHost host) : base(host)
        {
        }
        public async Task<int> IncreaseAsync(int count)
        {
            var counter = 0;
            var existing = await StateManager.TryGetStateAsync<int>(stateName: _stateName);
            if (existing.HasValue)
            {
                counter = existing.Value;
            }
            counter += count;
            await StateManager.SetStateAsync(stateName: _stateName, value: counter);
            await SaveStateAsync();
            return counter;
        }
        public async Task ResetAsync()
        {
            await StateManager.TryRemoveStateAsync(stateName: _stateName);
            await SaveStateAsync();
        }
    }
}
