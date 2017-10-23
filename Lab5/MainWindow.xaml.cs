using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private String _userName = "None";
        public String UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private String _userEmail = "None";
        public String UserEmail
        {
            get { return _userEmail; }
            set
            {
                _userEmail = value;
                OnPropertyChanged();
            }
        }

        User selectedUser;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            UsersListBox.Items.Add(new User(NameTextBox.Text, EmailTextBox.Text));
            UsersListBox.DisplayMemberPath = "Name";
            AdminsListBox.DisplayMemberPath = "Name";
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedIndex != -1)
            {
                UsersListBox.Items[UsersListBox.SelectedIndex] = new User(NameTextBox.Text, EmailTextBox.Text);
            }
            else
            {
                AdminsListBox.Items[AdminsListBox.SelectedIndex] = new User(NameTextBox.Text, EmailTextBox.Text);
            }
            RemoveButton.IsEnabled = false;
            MakeUserButton.IsEnabled = false;
            MakeAdminButton.IsEnabled = false;
            EditButton.IsEnabled = false;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            UsersListBox.Items.Remove(UsersListBox.SelectedItem);
            AdminsListBox.Items.Remove(AdminsListBox.SelectedItem);

            RemoveButton.IsEnabled = false;
            MakeUserButton.IsEnabled = false;
            MakeAdminButton.IsEnabled = false;
            EditButton.IsEnabled = false;
        }

        private void UsersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveButton.IsEnabled = true;
            MakeAdminButton.IsEnabled = true;
            EditButton.IsEnabled = true;

            selectedUser = (User)UsersListBox.SelectedItem;
            if (selectedUser != null)
            {
                UserName = selectedUser.Name;
                UserEmail = selectedUser.Email;
            }
            else
            {
                UserName = "None";
                UserEmail = "None";
            }
        }

        private void AdminsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveButton.IsEnabled = true;
            MakeUserButton.IsEnabled = true;
            EditButton.IsEnabled = true;

            selectedUser = (User)AdminsListBox.SelectedItem;
            if (selectedUser != null)
            {
                UserName = selectedUser.Name;
                UserEmail = selectedUser.Email;
            }
            else
            {
                UserName = "None";
                UserEmail = "None";
            }
        }

        private void MakeAdminButton_Click(object sender, RoutedEventArgs e)
        {
            AdminsListBox.Items.Add(UsersListBox.SelectedItem);
            UsersListBox.Items.Remove(UsersListBox.SelectedItem);
            MakeAdminButton.IsEnabled = false;
            RemoveButton.IsEnabled = false;
            EditButton.IsEnabled = false;
        }

        private void MakeUserButton_Click(object sender, RoutedEventArgs e)
        {
            UsersListBox.Items.Add(AdminsListBox.SelectedItem);
            AdminsListBox.Items.Remove(AdminsListBox.SelectedItem);
            MakeUserButton.IsEnabled = false;
            RemoveButton.IsEnabled = false;
            EditButton.IsEnabled = false;
        }

        private void EmailTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (EmailTextBox.Text == "Email")
            {
                EmailTextBox.Text = "";

            }
        }

        private void NameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "Name")
            {
                NameTextBox.Text = "";

            }
        }
    }
}
