using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow(string username, string profileImagePath, string favoriteQuote)
        {
            InitializeComponent();

            UsernameText.Text = username;
            FavoriteQuoteText.Text = favoriteQuote;

            if (!string.IsNullOrEmpty(profileImagePath) && File.Exists(profileImagePath))
            {
                try
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(System.IO.Path.GetFullPath(profileImagePath), UriKind.Absolute);
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; 
                    bitmap.EndInit();

                    ProfileImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Image file not found: " + profileImagePath);
            }
        }


        public MainWindow() : this("Guest", "", "Welcome!")
        {
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
