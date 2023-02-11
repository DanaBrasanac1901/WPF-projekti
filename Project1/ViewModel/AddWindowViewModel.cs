using GalaSoft.MvvmLight;
using Project1.Model;
using Project1.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project1.ViewModel
{
    public class AddWindowViewModel : ViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        private readonly DelegateCommand _addCommand;
        public ICommand AddCommand => _addCommand;

        private readonly DelegateCommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand;




        public UserRepository userRepository;

        public AddWindowViewModel() {

            userRepository = new UserRepository();
            _addCommand = new DelegateCommand(AddUser);
            _cancelCommand = new DelegateCommand(OnCancelClick);
        }

        public void AddUser(object commandParameter)
        {
            
            User user = new User(-1, UserName, Password, IsAdmin);
            userRepository.AddUser(user);
            MainWindow main = new MainWindow();
            main.Show();
            Close();
           
            
        }

        public void OnCancelClick(object commandParameter)
        {
            MainWindow main = new MainWindow();
            main.Show();

            Close();
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
