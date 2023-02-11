using GalaSoft.MvvmLight;
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

namespace Project2.ViewModel
{
    public class AddProductViewModel : ViewModelBase
    {
        private Repository Repository { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }

        private readonly DelegateCommand _addCommand;
        public ICommand AddCommand => _addCommand;

        private readonly DelegateCommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand;



        public AddProductViewModel()
        {


            Repository = new Repository();
            _addCommand = new DelegateCommand(OnAddClick);
            _cancelCommand = new DelegateCommand(OnCancelClick);


        }


        public void OnCancelClick(object commandParameter)
        {
            Window adminmain = new AdminMain();
            adminmain.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adminmain.Show();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }

        }

        public void OnAddClick(object commandParameter)
        {
            Product product = new Product(-1, Price, Name, "None", false, DateTime.Now.AddMinutes(2));
            Repository.AddProduct(product);
            Window adminmain = new AdminMain();
            adminmain.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adminmain.Show();
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
