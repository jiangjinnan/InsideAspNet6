using Microsoft.Extensions.ObjectPool;

namespace App
{
    public class FoobarPolicy : IPooledObjectPolicy<FoobarService>
    {
        public FoobarService Create() => FoobarService.Create();
        public bool Return(FoobarService obj) => true;
    }
}