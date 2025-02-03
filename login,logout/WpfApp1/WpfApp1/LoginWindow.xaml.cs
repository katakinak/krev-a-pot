using System;
using System.IO;
using System.Windows;

namespace WpfApp1
{
    public partial class LoginWindow : Window
    {
        private const string UsersFilePath = "./users.csv";
        private const string ProfileImagesFolder = "ProfileImages";
        private string selectedProfileImagePath = "";

        public LoginWindow()
        {
            InitializeComponent();

           
            if (!File.Exists(UsersFilePath))
            {
                File.Create(UsersFilePath).Dispose();
            }

            if (!Directory.Exists(ProfileImagesFolder))
            {
                Directory.CreateDirectory(ProfileImagesFolder);
            }
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            string username = SignUpUsername.Text.Trim();
            string password = SignUpPassword.Password.Trim();
            string favoriteQuote = FavoriteQuote.Text.Trim();

          
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(favoriteQuote) ||
                string.IsNullOrWhiteSpace(selectedProfileImagePath))
            {
                SignUpErrorMessage.Text = "All fields must be filled!";
                return;
            }

            
            if (IsUsernameTaken(username))
            {
                SignUpErrorMessage.Text = "Username already exists!";
                return;
            }

            
            string newProfileImagePath = Path.Combine(ProfileImagesFolder, Path.GetFileName(selectedProfileImagePath));
            if (!Directory.Exists(ProfileImagesFolder))
            {
                Directory.CreateDirectory(ProfileImagesFolder);
            }
            try
            {
                File.Copy(selectedProfileImagePath, newProfileImagePath, true);
            }
            catch (Exception ex)
            {
                SignUpErrorMessage.Text = $"Error saving profile image: {ex.Message}";
                return;
            }

           
            try
            {
                using (StreamWriter writer = new StreamWriter(UsersFilePath, true)) 
                {
                    writer.WriteLine($"{username},{password},{newProfileImagePath},{favoriteQuote}");
                }



                SignUpUsername.Clear();
                SignUpPassword.Clear();
                FavoriteQuote.Clear();
                selectedProfileImagePath = "";
                SelectedFilePath.Text = "No file selected";

                SignUpErrorMessage.Text = "Account created successfully!";
            }
            catch (Exception ex)
            {
                SignUpErrorMessage.Text = $"Error saving user data: {ex.Message}";
            }
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            string username = SignInUsername.Text.Trim();
            string password = SignInPassword.Password.Trim();

           
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                SignInErrorMessage.Text = "All fields must be filled!";
                return;
            }

            
            try
            {
                bool userFound = false;

                foreach (var line in File.ReadAllLines(UsersFilePath))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4 && parts[0] == username && parts[1] == password)
                    {
                        string profileImagePath = parts[2];
                        string favoriteQuote = parts[3];

                        
                        MainWindow mainWindow = new MainWindow(username, profileImagePath, favoriteQuote);
                        mainWindow.Show();
                        this.Close();

                        userFound = true;
                        break;
                    }
                }

                if (!userFound)
                {
                    SignInErrorMessage.Text = "Invalid username or password!";
                }
            }
            catch (Exception ex)
            {
                SignInErrorMessage.Text = $"Error: {ex.Message}";
            }
        }

        private void ChooseProfilePictureButton_Click(object sender, RoutedEventArgs e)
        {
          
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                selectedProfileImagePath = openFileDialog.FileName;
                SelectedFilePath.Text = System.IO.Path.GetFileName(selectedProfileImagePath);
            }
        }

        /// <summary>
        /// doplnit users, propojit s profileimagepath (asi)
        /// </summary>
        
        private bool IsUsernameTaken(string username)
        {
            foreach (var line in File.ReadAllLines(UsersFilePath))
            {
                var parts = line.Split(',');
                if (parts.Length >= 1 && parts[0] == username)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
