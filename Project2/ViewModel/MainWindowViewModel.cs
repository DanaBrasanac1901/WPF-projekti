using GalaSoft.MvvmLight;
using Project2.Model;
using Project2.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project2.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Product> Products { get; set; }
        private Repository Repository { get; set; }
        
        private readonly DelegateCommand _loginCommand;
        public ICommand LoginCommand => _loginCommand;


        public MainWindowViewModel() {
        
        Repository= new Repository();
        Products = Repository.Products;
        _loginCommand = new DelegateCommand(OnLoginClick);

        }

        public void OnLoginClick(object commandParameter)
        {
            Window login = new LoginWindow();
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.Show();
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

    }
}
