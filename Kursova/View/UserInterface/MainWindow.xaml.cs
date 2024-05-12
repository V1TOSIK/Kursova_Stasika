using Kursova.Modul;
using Kursova.Modul.Data;
using Kursova.View.UserInterface.Pages;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Kursova.View.UserInterface
{
  public partial class MainWindow : Window
  {
    private MyDBContext context;
    private UserData user;
    private DispatcherTimer timer;
    private DateTime firstDateTime;
    private ProductPage productPage;
    private CatalogPage catalogPage;
    private GlobalCatalogPage globalCatalogPage;
    public MainWindow(UserData user, MyDBContext context)
    {
      InitializeComponent();
      this.context = context;
      this.user = user;
      firstDateTime = DateTime.Today;
      InitializeTimer();
      
    }
    private void InitializeTimer()
    {

      timer = new DispatcherTimer();
      timer.Interval = TimeSpan.FromSeconds(1);
      timer.Tick += Timer_Tick;
      timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      DateTime newDate = DateTime.Today;
      if (productPage == null || firstDateTime != newDate) productPage = new ProductPage(context, user);
      if (catalogPage == null || firstDateTime != newDate) catalogPage = new CatalogPage(context, user);
      if (globalCatalogPage == null || firstDateTime != newDate) globalCatalogPage = new GlobalCatalogPage(context, user);
    }
    private void product_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
      NavigatePage(productPage);
    }
    private void Catalog_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
    NavigatePage(new CatalogPage(context, user));
    }
    private void GlobalCatalog_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
      NavigatePage(new GlobalCatalogPage(context, user));
    }
    private void NavigatePage(Page page)
    {
      Frame parentFrame = mainFrame;
      if (parentFrame != null)
      {
        parentFrame.Content = page;
      }

    }
  }

}
