namespace App
{
    public interface IFeatureCollection : IDictionary<Type, object?> { }

    public class FeatureCollection : Dictionary<Type, object?>, IFeatureCollection { }

    public static partial class Extensions
    {
        public static T? Get<T>(this IFeatureCollection features) where T : class
            => features.TryGetValue(typeof(T), out var value) ? (T?)value : default;

        public static IFeatureCollection Set<T>(this IFeatureCollection features, T? feature) where T : class
        {
            features[typeof(T)] = feature;
            return features;
        }
    }
}
