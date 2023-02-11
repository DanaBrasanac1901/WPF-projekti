using Project2.Model;
using Project2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using GalaSoft.MvvmLight;
using System.Collections;
using System.Windows.Data;

namespace Project2.ViewModel
{
    public class UserMainViewModel : ViewModelBase
    {
        public ObservableCollection<Product> Products { get; set; }
        public Product SelectedProduct { get; set; }
        private Repository Repository { get; set; }
        public string LoggedUserName { get; set; } 


        private readonly DelegateCommand _logoutCommand;
        public ICommand LogoutCommand => _logoutCommand;

        private readonly DelegateCommand _raiseCommand;
        public ICommand RaiseCommand => _raiseCommand;

       
        public UserMainViewModel(string username)
        {

            LoggedUserName= username;
            Repository = new Repository();
            AddActiveProducts();
            _logoutCommand = new DelegateCommand(OnLogoutClick);
            _raiseCommand = new DelegateCommand(OnRaiseClick);


        }

        public void AddActiveProducts()
        {
            Products = new ObservableCollection<Product>();
            foreach(Product product in Repository.Products)
            {
                if(!product.IsClosed) {
                    Products.Add(product);
                }
            }
        }

        public void OnLogoutClick(object commandParameter)
        {
            Window main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            main.Show();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }

        }

        public void OnRaiseClick(object commandParameter)
        {
            if (SelectedProduct.IsClosed) return;
            SelectedProduct.Price = SelectedProduct.Price + 1;
            SelectedProduct.Bidder = LoggedUserName;
            SelectedProduct.BidExpiration = DateTime.Now.AddMinutes(2);
            Repository.UpdateProduct(SelectedProduct);
            CollectionViewSource.GetDefaultView(Products).Refresh();


        }

        public void Close()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

    }
}
