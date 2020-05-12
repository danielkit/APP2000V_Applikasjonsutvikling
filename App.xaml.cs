using System.Windows;
using System.Data.Entity;

namespace test1
{
    public class Bruker
    {
        public string brukernavn { get; set; }
        public string passord { get; set; }
    }

    public class BlogDbContext: DbContext
    {
        public DbSet<Bruker> Brukers { get; set; }
    }

   public partial class App : Application
    {

    }
}