using GalaSoft.MvvmLight;
using Project1.Model;
using Project1.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project1.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
       
        public ObservableCollection<User> Users { get; set; }
        public User SelectedUser { get; set; }
        private UserRepository userRepository;


        //za dugme add u mainwindow
        private readonly DelegateCommand _addCommand;
        public ICommand AddCommand => _addCommand;


        //za dugme details u main window

        private readonly DelegateCommand _detailsCommand;
        public ICommand DetailsCommand => _detailsCommand;

       





        public MainWindowViewModel()
        {
            userRepository = new UserRepository();
            Users = userRepository.Users;
            _detailsCommand = new DelegateCommand(OnDetailsClick);
            _addCommand = new DelegateCommand(OnAddClick);
          
        }


        public void OnDetailsClick(object commandParameter)
        {
           
            Window details = new DetailsWindow();
            details.DataContext = new DetailsWindowViewModel(SelectedUser);
            details.Show();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }


        public void OnAddClick(object commandParameter)
        {
            Window add = new AddWindow();
            add.Show();
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }

        }


    }
}
