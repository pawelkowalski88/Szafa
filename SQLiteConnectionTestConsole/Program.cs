using System;
using DatabaseConnectionModule.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteDatabaseConnection;

namespace SQLiteConnectionTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("TEST");
            DatabaseConnectionService dbconn = new DatabaseConnectionService();

            List<clothes> clothesList = dbconn.GetClothes();

            foreach(var c in clothesList)
            {
                Console.WriteLine(c.id.ToString() + "; " + c.name);
            }
            Console.ReadLine();
        }
    }
}
