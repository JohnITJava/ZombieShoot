using System.Runtime.CompilerServices;

namespace ZombieQuest
{
    public class Test1
    {
        float health;
        public float publicHealth;
    }

    public class TestAction
    {
        float health;

        public static void Main(string[] args)
        {
            new TestAction().health = 100.00f;
            // new Test1().health = 100.00f;
            new Test1().publicHealth = 100.00f;
        }
    }
}