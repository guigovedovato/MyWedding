using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MyWeddingSystem.Models.Model;

namespace MyWeddingSystem.Models.Context
{
    public sealed class MyWeddingContext : DbContext
    {
        private static MyWeddingContext instance;

        public MyWeddingContext() : base("MyWedding") { }

        public static MyWeddingContext Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MyWeddingContext();
                }
                return instance;
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);

            modelBuilder.Properties<decimal>().Configure(c => c.HasPrecision(18, 5));
        }

        public DbSet<UserRepository> Users { get; set; }
        public DbSet<GuestRepository> Guests { get; set; }
        public DbSet<LogRepository> Logs { get; set; }
    }
}