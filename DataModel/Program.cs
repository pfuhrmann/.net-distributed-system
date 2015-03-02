using System.Data.Entity;

namespace DataModel
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Database.SetInitializer(new DatabaseContextInitializer());
        }
    }
}
