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
                if (AllData.Data.isEditing == false)
                {
                    using (RepairShopContext _db = new())
                    {
                        switch (AllData.Data.selectedTableIndex)
                        {
                            case 0:

                                _db.Cars.Add(new Car
                                {
                                    Brand = tbCarBrand.Text,
                                    HorsePower = Convert.ToInt32(tbCarHorsepower.Text),
                                    Model = tbCarModel.Text
                                });
                                _db.SaveChanges();

                                break;

                            case 1:

                                _db.Clients.Add(new Client
                                {
                                    Fio = tbClientsFio.Text,
                                    PhoneNumber = tbClientsPhoneNumber.Text,

                                    Birthday = (DateTime)dpClientsBirthday.SelectedDate
                                });
                                _db.SaveChanges();

                                break;

                            case 2:

                                _db.Workers.Add(new Worker
                                {
                                    Fio = tbWorkersFio.Text,
                                    PhoneNumber = tbWorkersPhoneNumber.Text,
                                    Experience = Convert.ToInt32(tbWorkersExperience.Text),
                                    Post = tbWorkersPost.Text,

                                    Birthday = (DateTime)dpWorkersBirthday.SelectedDate,
                                });
                                _db.SaveChanges();

                                break;

                            case 3:
                                var carId = _db.Cars.Where(x => x.Brand == (string)cbPartsAuto.SelectedValue)
                                                                        .Select(x => x.Id)
                                                                        .FirstOrDefault();
                                _db.Parts.Add(new Part
                                {
                                    PartName = tbPartsName.Text,
                                    PartCount = Convert.ToInt32(tbPartsCount.Text),
                                    PartSum = Convert.ToInt32(tbPartsSum.Text),
                                    SuitableCar = carId

                                });
                                _db.SaveChanges();

                                break;

                            case 4:
                                var clientId = _db.Clients.Where(x => x.Fio == (string)cbWorksClient.SelectedValue)
                                        .Select(x => x.Id)
                                        .FirstOrDefault();

                                var carId2 = _db.Cars.Where(x => x.Model == (string)cbWorksAuto.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                var workerId = _db.Workers.Where(x => x.Fio == (string)cbWorksWorker.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                _db.Works.Add(new Work
                                {
                                    Client = clientId,
                                    Worker = workerId,
                                    Car = carId2,

                                    SumOfWork = Convert.ToInt32(tbCoastOfWork.Text)
                                });
                                _db.SaveChanges();

                                break;
                        }
                    }

                }
                else
                {
                    using (RepairShopContext _db = new())
                    {
                        switch (AllData.Data.selectedTableIndex)
                        {
                            case 0:

                                _cars.Brand = tbCarBrand.Text;
                                _cars.HorsePower = Convert.ToInt32(tbCarHorsepower.Text);
                                _cars.Model = tbCarModel.Text;

                                _db.Cars.Update(_cars);

                                _db.SaveChanges();

                                break;

                            case 1:

                                _clients.Fio = tbClientsFio.Text;
                                _clients.PhoneNumber = tbClientsPhoneNumber.Text;

                                _clients.Birthday = (DateTime)dpClientsBirthday.SelectedDate;

                                _db.Clients.Update(_clients);

                                _db.SaveChanges();

                                break;

                            case 2:

                                _workers.Fio = tbWorkersFio.Text;
                                _workers.PhoneNumber = tbWorkersPhoneNumber.Text;
                                _workers.Experience = Convert.ToInt32(tbWorkersExperience.Text);
                                _workers.Post = tbWorkersPost.Text;

                                _workers.Birthday = (DateTime)dpWorkersBirthday.SelectedDate;

                                _db.Update(_workers);

                                _db.SaveChanges();

                                break;

                            case 3:

                                var carId = _db.Cars.Where(x => x.Brand == (string)cbPartsAuto.SelectedValue)
                                                                        .Select(x => x.Id)
                                                                        .FirstOrDefault();
                                _db.Parts.Update(new Part
                                {
                                    Id = _parts.Id,
                                    PartName = tbPartsName.Text,
                                    PartCount = Convert.ToInt32(tbPartsCount.Text),
                                    PartSum = Convert.ToInt32(tbPartsSum.Text),
                                    SuitableCar = carId
                                });

                                _db.SaveChanges();

                                break;

                            case 4:

                                var clientId = _db.Clients.Where(x => x.Fio == (string)cbWorksClient.SelectedValue)
                                        .Select(x => x.Id)
                                        .FirstOrDefault();

                                var carId2 = _db.Cars.Where(x => x.Model == (string)cbWorksAuto.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                var workerId = _db.Workers.Where(x => x.Fio == (string)cbWorksWorker.SelectedValue)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                var workId = _db.Works.Where(x => x.Id == _works.Id)
                                    .Select(x => x.Id)
                                    .FirstOrDefault();

                                _db.Works.Update(new Work
                                {
                                    Id = workId,
                                    Client = clientId,
                                    Worker = workerId,
                                    Car = carId2,

                                    SumOfWork = Convert.ToInt32(tbCoastOfWork.Text)
                                });

                                _db.SaveChanges();

                                break;
                        }
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
            Close();
        }


        private void btnAddPart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBoxWindow win = new();
                win.Title = "Введите ID запчасти";

                win.ShowDialog();

                int partId = Convert.ToInt32(AllData.Data.TbWindowResult);

                win = new();
                win.Title = "Введите количество потраченных запчастей";

                win.ShowDialog();

                int partCount = Convert.ToInt32(AllData.Data.TbWindowResult);

                using (RepairShopContext _db = new())
                {
                    _db.WorksParts.Add(new WorksPart
                    {
                        WorksId = _works.Id,
                        PartId = partId,
                        CountOfParts = partCount
                    });

                    _db.SaveChanges();
                }

                MessageBox.Show("Успешно!");
                Close();
            }
            catch (Exception ex)
            {
                ErrorEcho(ex);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                using (RepairShopContext _db = new())
                {
                    var car = _db.Cars.ToList();

                    foreach (var i in car)
                    {
                        cbPartsAuto.Items.Add(i.Brand);
                        cbWorksAuto.Items.Add(i.Model);
                    }

                    var client = _db.Clients.ToList();

                    foreach (var i in client)
                    {
                        cbWorksClient.Items.Add(i.Fio);
                    }

                    var worker = _db.Workers.ToList();

                    foreach (var i in worker)
                    {
                        cbWorksWorker.Items.Add(i.Fio);
                    }

                }

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

                            cbPartsAuto.SelectedValue = _parts.Brand;

                            break;

                        case 4:
                            gridWorks.Visibility = Visibility.Visible;

                            _works = AllData.Data.worksInformation;

                            tbCoastOfWork.Text = _works.SumOfWork.ToString();


                            cbWorksAuto.SelectedValue = _works.Car;

                            cbWorksClient.SelectedValue = _works.Client;

                            cbWorksWorker.SelectedValue = _works.Worker;

                            break;
                    }
                }
                else
                {
                    btnAddPart.IsEnabled = false;
                    btnAddPart.Content = "Добавить запчасть можно \nтолько при изменении";

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
