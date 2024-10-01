using Military_Elite.Core;
using Military_Elite.Core.Contracts;

namespace Military_Elite
{
    public  class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();

        }
    }
}