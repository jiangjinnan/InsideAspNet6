using Dapr.Client;
using Shared;
using System.Text;
using System.Text.Json;

namespace App2
{
public class ResultCache : IResultCache
{
    private readonly DaprClient _daprClient;
    private readonly string _keyOfKeys = "ResultKeys";
    private readonly string _storeName = "statestore";
    private readonly Func<string, Input, string> _keyGenerator;
    public ResultCache(DaprClient daprClient)
    {
        _daprClient = daprClient;
        _keyGenerator = (method, input) => $"ResultXX_{method}_{input.X}_{input.Y}";
    }
    public Task<Output> GetAsync(string method, Input input)
    {
        var key = _keyGenerator(method, input);
        return _daprClient.GetStateAsync<Output>(storeName: _storeName, key: key);
    }

    public async Task SaveAsync(string method, Input input, Output output)
    {
        var key = _keyGenerator(method, input);
        var keys = await _daprClient.GetStateAsync<HashSet<string>>(storeName: _storeName, key: _keyOfKeys) ?? new HashSet<string>();
        keys.Add(key);

        var operations = new StateTransactionRequest[2];
        var value = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(output));
        operations[0] = new StateTransactionRequest(key: key, value: value, operationType: StateOperationType.Upsert);

        value = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(keys));
        operations[1] = new StateTransactionRequest(key: _keyOfKeys, value: value, operationType: StateOperationType.Upsert);
        await _daprClient.ExecuteStateTransactionAsync(storeName: _storeName, operations: operations);
    }

    public async Task ClearAsync(params string[] methods)
    {
        var keys = await _daprClient.GetStateAsync<HashSet<string>>(storeName: _storeName, key: _keyOfKeys);
        if (keys != null)
        {
            var selectedKeys = keys.Where(it => methods.Any(m => it.StartsWith($"ResultXX_{m}"))).ToArray();
            if (selectedKeys.Length > 0)
            {
                var operations = selectedKeys
                    .Select(it => new StateTransactionRequest(key: it, value: null, operationType: StateOperationType.Delete))
                    .ToList();
                operations.ForEach(it => keys.Remove(it.Key));
                var value = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(keys));
                operations.Add(new StateTransactionRequest(key: _keyOfKeys, value: value, operationType: StateOperationType.Upsert));
                await _daprClient.ExecuteStateTransactionAsync(storeName: _storeName, operations: operations);
            }
        }
    }
}
}
