using Kursova.Modul;
using Kursova.Modul.Data;
using System.Linq;
using System.Windows.Controls;

namespace Kursova.View.UserInterface.Pages
{
  public partial class GlobalCatalogPage : Page
  {
    private MyDBContext context;
    private UserData user;
    public GlobalCatalogPage(MyDBContext context, UserData user)
    {
      InitializeComponent();
      this.context = context;
      this.user = user;

      LoadData();
    }
    private void LoadData()
    {
      var catalogData = from GlobalCatalogUser in context.Users
                        join product in context.Products on GlobalCatalogUser.Id equals product.UserId
                        select new GlobalCatalogData()
                        {
                          UserId = product.UserId,
                          UserName = GlobalCatalogUser.Name,
                          ProductName = product.Name,
                          Price = product.Price,
                          Volume = product.Volume,
                        };
      myDataGrid.ItemsSource = catalogData.ToList();
    }
  }
}
