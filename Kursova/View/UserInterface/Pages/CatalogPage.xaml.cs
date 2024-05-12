using Kursova.Modul;
using Kursova.Modul.Data;
using System.Linq;
using System.Windows.Controls;

namespace Kursova.View.UserInterface.Pages
{
  public partial class CatalogPage : Page
  {
    private MyDBContext context;
    private UserData user;
    public CatalogPage(MyDBContext context, UserData user)
    {
      InitializeComponent();
      this.context = context;
      this.user = user;

      LoadData();
    }
    private void LoadData()
    {
      var catalogData = from CatalogUser in context.Users
                         join product in context.Products on CatalogUser.Id equals product.UserId
                         where product.UserId == user.Id
                         select new CatalogData()
                         {
                           UserId = user.Id,
                           UserName = user.Name,
                           ProductName = product.Name,
                           Price = product.Price,
                           Volume = product.Volume,
                         };
      myDataGrid.ItemsSource = catalogData.ToList();
    }
  }
}
