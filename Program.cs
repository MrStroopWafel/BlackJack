using System;

namespace BlackJack
{
    class BlackJack
    {
        static void Main(string[] args)
        {
            Settings settings = new Settings();
            Table table = new Table(settings);
        }
    }
}
