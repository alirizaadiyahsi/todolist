using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Todo.Core.Database.Tables;

namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext()
             : base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<tblTask> tblTasks { get; set; }
        public DbSet<tblGroup> tblGroups { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
