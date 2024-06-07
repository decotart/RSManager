
using System.DirectoryServices.ActiveDirectory;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using RSManager.DataBase;
using RSManager.Windows;

namespace RSManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string login = tbLogin.Text,
                password = pbPass.Password;

                using (RepairShopContext db = new())
                {
                    try
                    {
                        var gen = db.Autorizations.Single(x => x.UserName == login);

                        if (gen != null && gen.UserPassword == password)
                        {
                            GeneralWindow win = new();
                            win.Show();
                            Close();
                        }
                        if (gen.UserPassword != password)
                        {
                            MessageBox.Show("Пароль не верный!");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Такого пользователя не существует!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            AllData.Data.isAddingUser = false;

            RegWindow win = new();
            win.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}