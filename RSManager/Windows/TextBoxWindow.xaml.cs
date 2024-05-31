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
using System.Windows.Shapes;

namespace RSManager.Windows
{
    /// <summary>
    /// Логика взаимодействия для TextBoxWindow.xaml
    /// </summary>
    public partial class TextBoxWindow : Window
    {
        public TextBoxWindow()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            AllData.Data.TbWindowResult = tbValue.Text;
            Close();
        }
    }
}
