namespace App
{
    public interface IFoobar { }
    public class Foobar : IFoobar
    {
        private Foobar() { }
        public static readonly Foobar Instance = new Foobar();
    }

}