using App;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var constructor = typeof(Foobarbaz).GetConstructors().Single();
var matcherType = typeof(ActivatorUtilities).GetNestedType("ConstructorMatcher", BindingFlags.NonPublic) ?? throw new InvalidOperationException("It fails to resove ConstructorMatcher type");
var matchMethod = matcherType.GetMethod("Match");

var foo = new Foo();
var bar = new Bar();
var baz = new Baz();
var qux = new Qux();

Console.WriteLine($"[Qux] = {Match(qux)}");

Console.WriteLine($"[Foo] = {Match(foo)}");
Console.WriteLine($"[Foo, Bar] = {Match(foo, bar)}");
Console.WriteLine($"[Foo, Bar, Baz] = {Match(foo, bar, baz)}");

Console.WriteLine($"[Bar, Baz] = {Match(bar, baz)}");
Console.WriteLine($"[Foo, Baz] = {Match(foo, baz)}");


int? Match(params object[] args)
{
    var matcher = Activator.CreateInstance(matcherType, constructor);
    return (int?)matchMethod?.Invoke(matcher, new object[] { args });
}

