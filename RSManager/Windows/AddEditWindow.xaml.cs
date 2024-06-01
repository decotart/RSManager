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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RSManager.DataBase;

namespace RSManager.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public AddEditWindow()
        {
            InitializeComponent();
        }

        Car? _cars;
        Work? _works;
        Worker? _workers;
        PartsBrandsView? _parts;
        Client? _clients;

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (AllData.Data.isEditing == true)
            {
                Title = "Изменить запись";
                btnAddEdit.Content = "Изменить";

                switch (AllData.Data.selectedTableIndex)
                {
                    case 0:
                        gridCars.Visibility = Visibility.Visible;

                        _cars = AllData.Data.carTable;

                        tbCarBrand.Text = _cars.Brand;
                        tbCarModel.Text = _cars.Model;
                        tbCarHorsepower.Text = _cars.HorsePower.ToString();

                        break;

                    case 1:
                        gridClients.Visibility = Visibility.Visible;

                        _clients = AllData.Data.clientTable;

                        tbClientsFio.Text = _clients.Fio;
                        tbClientsPhoneNumber.Text = _clients.PhoneNumber;
                        dpClientsBirthday.SelectedDate = _clients.Birthday;

                        break;

                    case 2:
                        gridWorkers.Visibility = Visibility.Visible;

                        _workers = AllData.Data.workerTable;

                        tbWorkersFio.Text = _workers.Fio;
                        tbWorkersPhoneNumber.Text = _workers.PhoneNumber;
                        tbWorkersPost.Text = _workers.Post;
                        tbWorkersExperience.Text = _workers.Experience.ToString();
                        dpWorkersBirthday.SelectedDate = _workers.Birthday;

                        break;

                    case 3:
                        gridParts.Visibility = Visibility.Visible;

                        _parts = AllData.Data.partsBrandsView;

                        tbPartsName.Text = _parts.PartName;
                        tbPartsCount.Text = _parts.PartCount.ToString();
                        tbPartsSum.Text = _parts.PartSum.ToString();

                        using (RepairShopContext _db = new())
                        {
                            var gen = _db.Cars.ToList();

                            foreach (var i in gen)
                            {
                                cbPartsAuto.Items.Add(i.Brand);
                            }
                        }

                        cbPartsAuto.SelectedItem = _parts.Brand;

                    break;

                    case 4:
                        gridWorks.Visibility = Visibility.Visible;


                        break;
                }
            }
        }
    }
}
