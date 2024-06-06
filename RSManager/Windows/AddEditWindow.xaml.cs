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
        WorksInformationEdited? _works;
        Worker? _workers;
        PartsBrandsView? _parts;
        Client? _clients;

        private void btnAddEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AllData.Data.isEditing == true)
                {
                    switch (AllData.Data.selectedTableIndex)
                    {
                        case 0:
                            using (RepairShopContext _db = new())
                            {
                                _db.Cars.Add(new Car
                                {
                                    Brand = tbCarBrand.Text,
                                    HorsePower = Convert.ToInt32(tbCarHorsepower.Text),
                                    Model = tbCarModel.Text
                                });
                                _db.SaveChanges();
                            }

                            break;

                        case 1:
                            _clients = new();

                            _clients.Fio = tbClientsFio.Text;
                            _clients.PhoneNumber = tbClientsPhoneNumber.Text;

                            _clients.Birthday = (DateTime)dpClientsBirthday.SelectedDate;

                            using (RepairShopContext _db = new())
                            {
                                _db.Clients.Add(_clients);
                                _db.SaveChanges();
                            }

                            break;

                        case 2:
                            _workers = new();

                            _workers.Fio = tbWorkersFio.Text;
                            _workers.PhoneNumber = tbWorkersPhoneNumber.Text;
                            _workers.Experience = Convert.ToInt32(tbWorkersExperience.Text);
                            _workers.Post = tbWorkersPost.Text;

                            _workers.Birthday = (DateTime)dpWorkersBirthday.SelectedDate;

                            using (RepairShopContext _db = new())
                            {
                                _db.Workers.Add(_workers);
                                _db.SaveChanges();
                            }

                            break;

                        case 3:
                            Part _parts = new();

                            _parts.PartName = tbPartsName.Text;
                            _parts.PartCount = Convert.ToInt32(tbPartsCount.Text);
                            _parts.PartSum = Convert.ToInt32(tbPartsSum.Text);

                            using (RepairShopContext _db = new())
                            {
                                var carId = _db.Cars.Where(x => x.Brand == (string)cbPartsAuto.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                _parts.SuitableCar = carId;

                                _db.Parts.Add(_parts);
                                _db.SaveChanges();
                            }

                            break;

                        case 4:
                            Work _works = new();

                            using (RepairShopContext _db = new())
                            {
                                var clientId = _db.Clients.Where(x => x.Fio == (string)cbWorksClient.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                var carId = _db.Cars.Where(x => x.Brand == (string)cbWorksAuto.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                var workerId = _db.Workers.Where(x => x.Fio == (string)cbWorksWorker.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                _works.Client = clientId;
                                _works.Worker = workerId;
                                _works.Car = carId;

                                _db.Works.Add(_works);
                                _db.SaveChanges();
                            }

                            break;

                    }

                }

                MessageBox.Show("Успешно!");
                Close();
            }
            catch (Exception ex)
            {
                ErrorEcho(ex);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

        }


        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
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

                            cbPartsAuto.SelectedValue = _parts.Brand;

                            break;

                        case 4:
                            gridWorks.Visibility = Visibility.Visible;

                            _works = AllData.Data.worksInformation;

                            tbCoastOfWork.Text = _works.SumOfWork.ToString();

                            using (RepairShopContext _db = new())
                            {
                                var gen = _db.WorksInformation.ToList();

                                foreach (var i in gen)
                                {
                                    cbWorksAuto.Items.Add(i.Car);

                                    cbWorksClient.Items.Add(i.Client);

                                    cbWorksWorker.Items.Add(i.Worker);
                                }

                                cbWorksAuto.SelectedValue = _works.Car;

                                cbWorksClient.SelectedValue = _works.Worker;

                                cbWorksWorker.SelectedValue = _works.Worker;
                            }

                            break;
                    }
                }
                else
                {
                    switch (AllData.Data.selectedTableIndex)
                    {
                        case 0:
                            gridCars.Visibility = Visibility.Visible;

                            break;

                        case 1:
                            gridClients.Visibility = Visibility.Visible;

                            break;

                        case 2:
                            gridWorkers.Visibility = Visibility.Visible;

                            break;

                        case 3:
                            gridParts.Visibility = Visibility.Visible;

                            break;

                        case 4:
                            gridWorks.Visibility = Visibility.Visible;

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorEcho(ex);
            }
        }

        public void ErrorEcho(Exception exception)
        {
            MessageBox.Show($"Что-то пошло не так! Сообщение об ошибке: {exception.Message}");
        }
    }
}
