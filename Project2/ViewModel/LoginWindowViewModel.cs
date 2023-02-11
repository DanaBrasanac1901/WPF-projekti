using GalaSoft.MvvmLight;
using Project2.Model;
using Project2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project2.ViewModel
{
    public class LoginWindowViewModel : ViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Repository Repository { get; set; }

        public bool IsUserAdmin { get; set; }

        private readonly DelegateCommand _loginCommand;

        public ICommand LoginCommand => _loginCommand;

        private readonly DelegateCommand _cancelCommand;

        public ICommand CancelCommand => _cancelCommand;




        public LoginWindowViewModel()
        {
            Repository = new Repository();
            _loginCommand = new DelegateCommand(Login);
            _cancelCommand = new DelegateCommand(OnCancelClick);
            IsUserAdmin = false;
        }

        public void OnCancelClick(object commandParameter)
        {
            MainWindow main = new MainWindow();
            main.WindowStartupLocation = WindowStartupLocation.CenterScreen;
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


        public void Login(object commandParameter)
        {

            if (CheckCredentials())
            {
                if (IsUserAdmin)
                {
                    Window adminWindow = new AdminMain();
                    adminWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    adminWindow.Show();
                }
                else
                {
                    Window userWindow = new UserMain();
                    userWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    userWindow.DataContext = new UserMainViewModel(UserName);
                    userWindow.Show();  
                }
             
                foreach (Window item in Application.Current.Windows)
                {
                    if (item.DataContext == this) item.Close();
                }
            }
            else
            {
                MessageBox.Show("Login failed. Please try again.");
            }

        }

        public bool CheckCredentials()
        {
            List<User> users = Repository.Users.ToList();
            foreach (User user in users)
            {
                if (user.UserName == UserName)
                {
                    if (user.UserPass == Password)
                    {
                        if (user.IsAdmin) IsUserAdmin= true;
                        else IsUserAdmin= false;
                        return true;

                    }
                }
            }
            return false;
        }
    }
}
