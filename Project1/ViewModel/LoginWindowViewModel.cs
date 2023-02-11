using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Project1.Model;
using Project1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project1.ViewModel
{
    public class LoginWindowViewModel : ViewModelBase
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserRepository UserRepository { get; set; }

        private readonly DelegateCommand _loginCommand;
       
        public ICommand LoginCommand => _loginCommand;
       

      

        public LoginWindowViewModel() {
             UserRepository= new UserRepository();
            _loginCommand = new DelegateCommand(Login);
        }


            public void Login(object commandParameter)
        {

            if (CheckCredentials())
            {
                Window window = new MainWindow();
                window.Show();
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
            List<User> users = UserRepository.Users.ToList();
            foreach (User user in users)
            {
                if (user.UserName == UserName)
                {
                    if (user.UserPass == Password)
                    {
                        return true;

                    }
                }
            }
            return false;
        }
    }
}
