using Kursova.Modul.Data;
using System.Data.Entity;

namespace Kursova.Modul
{
  public class MyDBContext : DbContext
  {
    public MyDBContext() : base("DBConnectionString")
    {

    }
    public DbSet<UserData> Users {  get; set; }
    public DbSet<Products> Products { get; set; }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
      modelBuilder.Entity<UserData>()
          .HasMany(d => d.Product)
          .WithRequired(d => d.User)
          .HasForeignKey(a => a.UserId)
          .WillCascadeOnDelete(false);
    }
  }
}
