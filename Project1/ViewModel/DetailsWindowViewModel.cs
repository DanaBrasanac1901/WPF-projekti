using Project1.Model;
using Project1.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Project1.ViewModel
{
    public class DetailsWindowViewModel
    {
        public User SelectedUser { get; set; }
        private UserRepository userRepository;
       

        private readonly DelegateCommand _saveCommand;
        public ICommand SaveCommand => _saveCommand;

        private readonly DelegateCommand _cancelCommand;
        public ICommand CancelCommand => _cancelCommand;



        public DetailsWindowViewModel(User selectedUser)
        {
            SelectedUser = selectedUser;
            _saveCommand = new DelegateCommand(OnSave);
            userRepository = new UserRepository();
            _cancelCommand = new DelegateCommand(OnCancelClick);
        }

        public void OnCancelClick(object commandParameter)
        {
            MainWindow main = new MainWindow();
            main.Show();
            Close();
        }

        public void OnSave(object commandParameter)
        {
            userRepository.UpdateUser(SelectedUser);
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
