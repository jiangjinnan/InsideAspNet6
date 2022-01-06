using Grpc.Core;

namespace App
{
    public class CalculatorService : Calculator.CalculatorBase
    {
        private readonly ILogger _logger;
        public CalculatorService(ILogger<CalculatorService> logger) => _logger = logger;
        public override Task<OutpuMessage> Add(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 + op2, request);
        public override Task<OutpuMessage> Substract(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 - op2, request);
        public override Task<OutpuMessage> Multiply(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 * op2, request);
        public override Task<OutpuMessage> Divide(InputMessage request, ServerCallContext context)
            => InvokeAsync((op1, op2) => op1 / op2, request);

        private Task<OutpuMessage> InvokeAsync(Func<int, int, int> calculate, InputMessage input)
        {
            OutpuMessage output;
            try
            {
                output = new OutpuMessage { Status = 0, Result = calculate(input.X, input.Y) };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Calculation error.");
                output = new OutpuMessage { Status = 1, Error = ex.ToString() };
            }
            return Task.FromResult(output);
        }
    }
}