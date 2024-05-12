using Kursova.Modul;
using Kursova.Modul.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Converters;

namespace Kursova.View.UserInterface.Pages
{
  public partial class ProductPage : Page
  {
    private MyDBContext context;
    private UserData user;
    public ProductPage(MyDBContext context, UserData user)
    {
      InitializeComponent();
      this.context = context;
      this.user = user;

    }
    public void SaveDataButton_Click(object sender, RoutedEventArgs e) {
      try
      {
        if( user == null || user.Product == null || user.Product.Count == 0)
        {
          user.Product = new List<Products>();
        }
        var name = ProductNamebox.inputText.Text;
        var existingProduct = context.Products.FirstOrDefault(a => a.Name == name && a.UserId == user.Id);

        if (existingProduct != null) { UpdateExistingProduct(existingProduct); }
        else CreateNewProduct();
      }
      catch (Exception ex)
      {

        MessageBox.Show($"error: {ex}");
      }

    }
    public string ChekingProducts(out string productName, out double productPrice, out int productVolume)
    {
      string message = "";
      if (string.IsNullOrEmpty(ProductNamebox.inputText.Text)) {
        productName = string.Empty;
        message += "Назва продукту обов'язкова\n"; }
      else { productName = ProductNamebox.inputText.Text.ToLower().Trim(); }

      if (double.TryParse(Pricebox.inputText.Text.Trim(), out productPrice))
      {
        if (productPrice < 0) { message += "Ціна не може бути від'ємною\n"; }
      }
      if (int.TryParse(Volumebox.inputText.Text.Trim(), out productVolume)) {
        if (productVolume < 0) { message += "Кількість товару не може бути від'ємною!\n"; }
      }
      return message;
    }
    private void UpdateExistingProduct(Products products)
    {
      string message = ChekingProducts(out string productName, out double productPrice, out int productVolume);

      if (message == string.Empty)
      {
        products.Name = productName;
        products.Price = productPrice;
        products.Volume = productVolume;
        context.SaveChanges();
        ClearText();
      }
      else
      {
        MessageBox.Show(message);
      }
    }
    private void CreateNewProduct()
    {
      string message = ChekingProducts(out string productName, out double productPrice, out int productVolume);

      if (message == string.Empty)
      {
        user.Product.Add(new Products()
        {
          Name = productName,
          Price = productPrice,
          Volume = productVolume,
        });
        context.SaveChanges();
        ClearText();
      }
      else
      {
        MessageBox.Show(message);
      }
    }
    private void ClearText()
    {
      ProductNamebox.inputText.Text = string.Empty;
      Pricebox.inputText.Text = string.Empty;
      Volumebox.inputText.Text = string.Empty;
    }
  }
}
