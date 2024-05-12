using System.Windows.Documents;

namespace Kursova.Modul.Data
{
  public class Products
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Volume { get; set; }
    public int UserId { get; set; }
    public virtual UserData User {  get; set; }
  }
}
