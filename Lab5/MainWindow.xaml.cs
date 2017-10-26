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
using System.Globalization;
using System.Text.RegularExpressions;

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
            var nameRaw = NameTextBox.Text.ToLower().Trim();
            var email = EmailTextBox.Text.ToLower().Trim();

            TextInfo nameTitle = new CultureInfo("sv-SE", false).TextInfo;

            var name = nameTitle.ToTitleCase(nameRaw);

            if (ValidUserInput(name, email))
            {
                UsersListBox.Items.Add(new User(name, email));
                UsersListBox.DisplayMemberPath = "Name";
                AdminsListBox.DisplayMemberPath = "Name";
            }
        }



        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersListBox.SelectedIndex != -1)
            {
                var nameRaw = NameTextBox.Text.ToLower().Trim();
                var email = EmailTextBox.Text.ToLower().Trim();

                TextInfo nameTitle = new CultureInfo("sv-SE", false).TextInfo;

                var name = nameTitle.ToTitleCase(nameRaw);

                if (ValidUserInput(name, email))
                {
                    int index = UsersListBox.SelectedIndex;
                    UsersListBox.Items[index] = new User(name, email);
                    UsersListBox.SelectedIndex = index;
                }
            }
            else
            {
                var nameRaw = NameTextBox.Text.ToLower().Trim();
                var email = EmailTextBox.Text.ToLower().Trim();

                TextInfo nameTitle = new CultureInfo("sv-SE", false).TextInfo;

                var name = nameTitle.ToTitleCase(nameRaw);

                if (ValidUserInput(name, email))
                {
                    int index = AdminsListBox.SelectedIndex;
                    AdminsListBox.Items[index] = new User(name, email);
                    AdminsListBox.SelectedIndex = index;
                }
            }
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

            var position = UsersListBox.SelectedIndex;
            AdminsListBox.SelectedIndex = -1;
            UsersListBox.SelectedIndex = position;

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

            var position = AdminsListBox.SelectedIndex;
            UsersListBox.SelectedIndex = -1;
            AdminsListBox.SelectedIndex = position;

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

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            UsersListBox.Items.Add(new User("Otto", "otto.r@protonmail.ch"));
            UsersListBox.Items.Add(new User("Dave", "DaveDavidson@Dmail.com"));
            UsersListBox.Items.Add(new User("John", "John@Doe.ca"));
            UsersListBox.Items.Add(new User("Jeanne", "J@mail.fr"));
            UsersListBox.Items.Add(new User("Vyacheslav", "V.M.Molotov@PeopleCommissariatforForeignAffairs.su"));
            UsersListBox.Items.Add(new User("Joe", "J.Sixpack@USA.us"));
            UsersListBox.DisplayMemberPath = "Name";
            AdminsListBox.DisplayMemberPath = "Name";
        }
        private bool ValidUserInput(string name, string email)
        {
            Match matchName = Regex.Match(name, @"^[a-zA-ZåäöÅÄÖ]*$");
            if (matchName.Success && name != "Name" && !string.IsNullOrWhiteSpace(name))
            {
                Match matchEmail = Regex.Match(email, ".@.");
                if (matchEmail.Success && !string.IsNullOrWhiteSpace(email) && email != "email")
                {
                    return true;
                }
                else
                {
                    if (string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Please enter a valid email address." + Environment.NewLine + "- Empty entries not allowed");
                        return false;
                    }
                    else
                    {
                        MessageBox.Show("Please enter a valid email address." + Environment.NewLine + "- Ex: Name@mail.com");
                        return false;
                    }
                }
            }
            else
            {
                if (!matchName.Success)
                {
                    MessageBox.Show("Please enter a valid name." + Environment.NewLine + "- Illegal character used" + Environment.NewLine + "No numbers or special characters allowed.");
                    return false;
                }
                else if (name == "Name")
                {
                    MessageBox.Show("Please enter a valid name." + Environment.NewLine + "Name cannot be \"Name\"");
                    return false;
                }
                else
                {
                    MessageBox.Show("No empty entries allowed");
                    return false;
                }
            }
        }
    }
}
