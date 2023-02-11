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
using System.Configuration;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows.Data;

namespace Project2.ViewModel
{
    public class AdminMainViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Product> Products { get; set; }
        public Product SelectedProduct { get; set ; }
        private Repository Repository { get; set; }

        private readonly DelegateCommand _logoutCommand;
        public ICommand LogoutCommand => _logoutCommand;

        private readonly DelegateCommand _addCommand;
        public ICommand AddCommand => _addCommand;

        private readonly DelegateCommand _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand;



        public AdminMainViewModel() {


            Repository = new Repository();
            Products = Repository.Products;
            _logoutCommand = new DelegateCommand(OnLogoutClick);
            _addCommand = new DelegateCommand(OnAddClick);
            _deleteCommand = new DelegateCommand(OnDeleteClick);
         

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

        public void OnAddClick(object commandParameter)
        {
            Window add = new AddProductWindow();
            add.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            add.Show();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }

        }

        public void Close()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }


        public void OnDeleteClick(object commandParameter)
        {
            Repository.DeleteProduct(SelectedProduct);
            Products.Remove(SelectedProduct);
        }
    }
}
