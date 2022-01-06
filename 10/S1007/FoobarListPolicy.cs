using Microsoft.Extensions.ObjectPool;

namespace App
{
    public class FoobarListPolicy : PooledObjectPolicy<List<Foobar>>
    {
        private readonly int _initCapacity;
        private readonly int _maxCapacity;

        public FoobarListPolicy(int initCapacity, int maxCapacity)
        {
            _initCapacity = initCapacity;
            _maxCapacity = maxCapacity;
        }
        public override List<Foobar> Create() => new(_initCapacity);
        public override bool Return(List<Foobar> obj)
        {
            if (obj.Capacity < _maxCapacity)
            {
                obj.Clear();
                return true;
            }
            return false;
        }
    }
}