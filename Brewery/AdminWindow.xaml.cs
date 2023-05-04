using BreweryAPI.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Data.SqlClient;
using Microsoft.Win32;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Document = DocumentFormat.OpenXml.Wordprocessing.Document;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;
using ScottPlot;
using System.Net.Http;

namespace Brewery
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public string tabName = "";
        public string Token = "";
        HubConnection connection;  // подключение для взаимодействия с хабом
        public AdminWindow(string token)
        {
            InitializeComponent();
            Token = token;

            // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            // Создание экземпляра HttpClient с настроенным HttpClientHandler.
            var httpClient = new HttpClient(handler);

            // Создание экземпляра DelegatingHandler с настроенным HttpClient.
            var messageHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };


            // создаем подключение к хабу
            connection = new HubConnectionBuilder()
                .WithUrl("https://172.20.10.2:7201/hub", options =>
                {
                    options.HttpMessageHandlerFactory = _ => messageHandler;
                })
                .Build();

            connection.On<IEnumerable<Admin>?>("getAdmins", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        try
                        {
                            dgAdmin.ItemsSource = value;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Can't get the admin values.");
                    }
                });
            });

            connection.On<IEnumerable<AdminList>?>("getAdminsLists", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgAdminList.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the adminlist values.");
                    }
                });
            });

            connection.On<IEnumerable<BeerCheque>?>("getBeerCheques", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBeer_Cheque.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the beercheques values.");
                    }
                });
            });

            connection.On<IEnumerable<Beer>?>("getBeers", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBeer.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the beer values.");
                    }
                });
            });

            connection.On<IEnumerable<BeerList>?>("getBeersLists", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBeerList.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the beerslist values.");
                    }
                });
            });

            connection.On<IEnumerable<BeerType>?>("getBeerTypes", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBeerType.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the beertypes values.");
                    }
                });
            });

            connection.On<IEnumerable<Breweries>?>("getBreweries", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBrewery.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the breweries values.");
                    }
                });
            });

            connection.On<IEnumerable<BreweryList>?>("getBreweriesLists", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBreweryList.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the brewerieslists values.");
                    }
                });
            });

            connection.On<IEnumerable<BreweryBeer>?>("getBreweryBeers", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBrewery_Beer.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the brewerybeer values.");
                    }
                });
            });

            connection.On<IEnumerable<BreweryIngridient>?>("getBreweryIngridients", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgBrewery_Ingridients.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the breweryingridients values.");
                    }
                });
            });

            connection.On<IEnumerable<Cheque>?>("getCheques", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgCheque.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the cheques values.");
                    }
                });
            });

            connection.On<IEnumerable<ChequeList>?>("getChequesLists", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgChequeList.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the chequeslists values.");
                    }
                });
            });

            connection.On<IEnumerable<Ingridient>?>("getIngridients", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgIngridients.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the ingridients values.");
                    }
                });
            });

            connection.On<IEnumerable<IngridientsBeer>?>("getIngridientsBeers", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgIngridientsBeer.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the ingridientsbeers values.");
                    }
                });
            });

            connection.On<IEnumerable<IngridientsList>?>("getIngridientsLists", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgIngridientsList.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the ingridientslists values.");
                    }
                });
            });

            connection.On<IEnumerable<IngridientsType>?>("getIngridientsTypes", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgIngridientsType.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the ingridientstypes values.");
                    }
                });
            });

            connection.On<IEnumerable<Subscription>?>("getSubscriptions", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgSubscription.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the subscriprions values.");
                    }
                });
            });

            connection.On<IEnumerable<Supplier>?>("getSuppliers", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgSupplier.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the subscriprions values.");
                    }
                });
            });

            connection.On<IEnumerable<SuppliersList>?>("getSuppliersLists", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgSuppliersList.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the supplierslists values.");
                    }
                });
            });

            connection.On<IEnumerable<User>?>("getUsers", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgUser.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the users values.");
                    }
                });
            });

            connection.On<IEnumerable<UserList>?>("getUsersLists", (value) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (value != null)
                    {
                        dgUserList.ItemsSource = value;
                    }
                    else
                    {
                        MessageBox.Show("Can't get the userslists values.");
                    }
                });
            });

            // устанавливаем соединение и запрашиваем данные при загрузке окна
            this.Loaded += (sender, e) =>
            {
                connection.StartAsync();
                connection.InvokeAsync("GetAdmins");
                connection.InvokeAsync("GetAdminsLists");
                connection.InvokeAsync("GetBeerCheques");
                connection.InvokeAsync("GetBeers");
                connection.InvokeAsync("GetBeersLists");
                connection.InvokeAsync("GetBeerTypes");
                connection.InvokeAsync("GetBreweries");
                connection.InvokeAsync("GetBreweriesLists");
                connection.InvokeAsync("GetBreweryBeers");
                connection.InvokeAsync("GetBreweryIngridients");
                connection.InvokeAsync("GetCheques");
                connection.InvokeAsync("GetChequesLists");
                connection.InvokeAsync("GetIngridients");
                connection.InvokeAsync("GetIngridientsBeers");
                connection.InvokeAsync("GetIngridientsLists");
                connection.InvokeAsync("GetIngridientsTypes");
                connection.InvokeAsync("GetSubscriptions");
                connection.InvokeAsync("GetSuppliers");
                connection.InvokeAsync("GetSuppliersLists");
                connection.InvokeAsync("GetUsers");
                connection.InvokeAsync("GetUsersLists");
            };
        }

        //---------------------------------------------UpdMethods---------------------------------------------------------
        private async void Update_Load()
        {
            await connection.InvokeAsync("GetAdmins");
            await connection.InvokeAsync("GetAdminsLists");
            await connection.InvokeAsync("GetBeerCheques");
            await connection.InvokeAsync("GetBeers");
            await connection.InvokeAsync("GetBeersLists");
            await connection.InvokeAsync("GetBeerTypes");
            await connection.InvokeAsync("GetBreweries");
            await connection.InvokeAsync("GetBreweriesLists");
            await connection.InvokeAsync("GetBreweryBeers");
            await connection.InvokeAsync("GetBreweryIngridients");
            await connection.InvokeAsync("GetCheques");
            await connection.InvokeAsync("GetChequesLists");
            await connection.InvokeAsync("GetIngridients");
            await connection.InvokeAsync("GetIngridientsBeers");
            await connection.InvokeAsync("GetIngridientsLists");
            await connection.InvokeAsync("GetIngridientsTypes");
            await connection.InvokeAsync("GetSubscriptions");
            await connection.InvokeAsync("GetSuppliers");
            await connection.InvokeAsync("GetSuppliersLists");
            await connection.InvokeAsync("GetUsers");
            await connection.InvokeAsync("GetUsersLists");
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpenTableReg();

            // Находим нужный TabItem по заголовку (Header)
            TabItem tabItem = FindTabItem(tabControl, tabName);

            // Устанавливаем свойство IsSelected элемента TabItem, чтобы открыть эту вкладку
            tabItem.IsSelected = true;

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("GetAdmins");
                await connection.InvokeAsync("GetAdminsLists");
                await connection.InvokeAsync("GetBeerCheques");
                await connection.InvokeAsync("GetBeers");
                await connection.InvokeAsync("GetBeersLists");
                await connection.InvokeAsync("GetBeerTypes");
                await connection.InvokeAsync("GetBreweries");
                await connection.InvokeAsync("GetBreweriesLists");
                await connection.InvokeAsync("GetBreweryBeers");
                await connection.InvokeAsync("GetBreweryIngridients");
                await connection.InvokeAsync("GetCheques");
                await connection.InvokeAsync("GetChequesLists");
                await connection.InvokeAsync("GetIngridients");
                await connection.InvokeAsync("GetIngridientsBeers");
                await connection.InvokeAsync("GetIngridientsLists");
                await connection.InvokeAsync("GetIngridientsTypes");
                await connection.InvokeAsync("GetSubscriptions");
                await connection.InvokeAsync("GetSuppliers");
                await connection.InvokeAsync("GetSuppliersLists");
                await connection.InvokeAsync("GetUsers");
                await connection.InvokeAsync("GetUsersLists");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            // Создание экземпляра HttpClient с настроенным HttpClientHandler.
            var httpClient = new HttpClient(handler);

            // Создание экземпляра DelegatingHandler с настроенным HttpClient.
            var messageHandler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };


            // создаем подключение к хабу
            connection = new HubConnectionBuilder()
                .WithUrl("https://172.20.10.2:7201/hub", options =>
                {
                    options.HttpMessageHandlerFactory = _ => messageHandler;
                })
                .Build();
            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("GetAdmins");
                await connection.InvokeAsync("GetAdminsLists");
                await connection.InvokeAsync("GetBeerCheques");
                await connection.InvokeAsync("GetBeers");
                await connection.InvokeAsync("GetBeersLists");
                await connection.InvokeAsync("GetBeerTypes");
                await connection.InvokeAsync("GetBreweries");
                await connection.InvokeAsync("GetBreweriesLists");
                await connection.InvokeAsync("GetBreweryBeers");
                await connection.InvokeAsync("GetBreweryIngridients");
                await connection.InvokeAsync("GetCheques");
                await connection.InvokeAsync("GetChequesLists");
                await connection.InvokeAsync("GetIngridients");
                await connection.InvokeAsync("GetIngridientsBeers");
                await connection.InvokeAsync("GetIngridientsLists");
                await connection.InvokeAsync("GetIngridientsTypes");
                await connection.InvokeAsync("GetSubscriptions");
                await connection.InvokeAsync("GetSuppliers");
                await connection.InvokeAsync("GetSuppliersLists");
                await connection.InvokeAsync("GetUsers");
                await connection.InvokeAsync("GetUsersLists");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //  Получаем имя активного TabItem.
            if (tabControl.SelectedItem != null)
            {
                TabItem selectedTab = tabControl.SelectedItem as TabItem;
                tabName = selectedTab.Header.ToString();
                SaveTableReg();
            }
        }


        //---------------------------------------------MainMethods---------------------------------------------------------
        public async void CreateTable<T>(DataGrid dataGrid)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                await connection.StartAsync();
                while (connection.State != HubConnectionState.Connected)
                {
                    await Task.Delay(100);
                }
                var selectedItems = dataGrid.SelectedItems.Cast<T>().ToList();
                if (selectedItems.Count == 1)
                {
                    var data = selectedItems.FirstOrDefault();
                    var properties = typeof(T).GetProperties();
                    properties[0].SetValue(data, null);
                    string ClassName = "Post" + typeof(T).Name;


                    await connection.InvokeAsync($"{ClassName}", data);
                    Update_Load();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void PutTable<T>(DataGrid dataGrid)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                await connection.StartAsync();
                while (connection.State != HubConnectionState.Connected)
                {
                    await Task.Delay(100);
                }
                var selectedItems = dataGrid.SelectedItems.Cast<T>().ToList();
                if (selectedItems.Count == 1)
                {
                    var data = selectedItems.FirstOrDefault();
                    var properties = typeof(T).GetProperties();
                    int ID = (int)properties[0].GetValue(data);
                    string ClassName = "Put" + typeof(T).Name;

                    await connection.InvokeAsync($"{ClassName}", ID, data);
                    Update_Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public async void DeleteTable<T>(DataGrid dataGrid)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                await connection.StartAsync();
                while (connection.State != HubConnectionState.Connected)
                {
                    await Task.Delay(100);
                }
                var selectedItems = dataGrid.SelectedItems.Cast<T>().ToList();
                if (selectedItems.Count == 1)
                {
                    var data = selectedItems.FirstOrDefault();
                    var properties = typeof(T).GetProperties();
                    int ID = (int)properties[0].GetValue(data);
                    string ClassName = "Delete" + typeof(T).Name;

                    await connection.InvokeAsync($"{ClassName}", ID);
                    Update_Load();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Exit()
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }
        public string ConvertIntArrayToString(int[] array)
        {
            string result = "[";

            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString();

                if (i < array.Length - 1)
                {
                    result += ",";
                }
            }

            result += "]";

            return result;
        }
        private async void LogicalDelete<T>(DataGrid dataGrid) where T : class
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                await connection.StartAsync();
                while (connection.State != HubConnectionState.Connected)
                {
                    await Task.Delay(100);
                }
                var selectedItems = dataGrid.SelectedItems.Cast<T>().ToList();
                List<T> itemsToDelete = new List<T>();
                foreach (var item in selectedItems)
                {
                    itemsToDelete.Add(item);
                }

                int[] idsToDelete = itemsToDelete.Select(x => (int)x.GetType().GetProperties()[0].GetValue(x)).ToArray();

                string ClassName = "LogicalDelete" + typeof(T).Name;
                await connection.InvokeAsync($"{ClassName}", idsToDelete);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private async void LogicalRestore<T>(DataGrid dataGrid) where T : class
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                await connection.StartAsync();
                while (connection.State != HubConnectionState.Connected)
                {
                    await Task.Delay(100);
                }
                var selectedItems = dataGrid.SelectedItems.Cast<T>().ToList();
                List<T> itemsToRestore = new List<T>();
                foreach (var item in selectedItems)
                {
                    itemsToRestore.Add(item);
                }

                int[] idsToRestore = itemsToRestore.Select(x => (int)x.GetType().GetProperties()[0].GetValue(x)).ToArray();

                string ClassName = "LogicalRestore" + typeof(T).Name;
                await connection.InvokeAsync($"{ClassName}", idsToRestore);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public List<T> GetDataFromGrid<T>(DataGrid dataGrid)
        {
            List<T> itemsSourceList = new List<T>();

            if (dataGrid.ItemsSource is IEnumerable<T> itemsSource)
            {
                itemsSourceList = itemsSource.ToList();
            }
            else if (dataGrid.ItemsSource is IEnumerable itemsSourceNonGeneric)
            {
                foreach (var item in itemsSourceNonGeneric)
                {
                    if (item is T tItem)
                    {
                        itemsSourceList.Add(tItem);
                    }
                }
            }
            return itemsSourceList;
        }
        public void ExportToExcel<T>(List<T> data)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Извлечение имени типа обобщенного класса
                var typeName = typeof(T).Name;

                // Создание листа
                var worksheet = package.Workbook.Worksheets.Add(typeName);

                // Получение списка свойств класса
                var properties = typeof(T).GetProperties();

                // Заполнение заголовков ячеек на листе
                for (int j = 0; j < properties.Length; j++)
                {
                    worksheet.Cells[1, j + 1].Value = properties[j].Name;
                }

                // Заполнение ячеек данными из списка
                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < properties.Length; j++)
                    {
                        var value = properties[j].GetValue(data[i], null);
                        worksheet.Cells[i + 2, j + 1].Value = value;
                    }
                }

                // Получение пути и имени файла для сохранения
                var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*",
                    FileName = $"{typeName} {DateTime.Now:yyyyMMddHHmmss}" // Использование имени класса и времени сохранения в качестве базового имени файла
                };

                if (saveFileDialog.ShowDialog() == true && !string.IsNullOrEmpty(saveFileDialog.FileName))
                {
                    var filePath = saveFileDialog.FileName;

                    // Сохранение файла
                    package.SaveAs(new FileInfo(filePath));
                }
            }
        }
        public void ExportToWord<T>(List<T> data)
        {
            // Извлечение имени типа обобщенного класса
            var typeName = typeof(T).Name;

            // Создание документа и задание пути сохранения
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = $"{typeName} {DateTime.Now:yyyyMMddHHmmss}.docx";
            saveFileDialog.Filter = "Word Documents (*.docx)|*.docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var document = WordprocessingDocument.Create(saveFileDialog.FileName, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
                {
                    // Создание тела документа
                    var body = new Body();

                    // Добавление параграфов с данными
                    foreach (var item in data)
                    {
                        var properties = typeof(T).GetProperties();
                        foreach (var property in properties)
                        {
                            var paragraph = new Paragraph();
                            var text = $"{property.Name}: {property.GetValue(item)?.ToString()}";
                            var run = new Run(new Text(text));
                            paragraph.Append(run);
                            body.Append(paragraph);
                        }
                        body.Append(new Paragraph()); // Добавление пустой строки после каждой записи
                    }

                    // Добавление тела документа в документ Word
                    var mainPart = document.AddMainDocumentPart();
                    mainPart.Document = new Document(body);
                    mainPart.Document.Save();
                }
            }
        }
        public void CreateBackup()
        {
            // строка подключения к локальной базе данных
            string connectionString = @"Data Source=FIIVA\DA;Initial Catalog=Brewery;TrustServerCertificate=True;Persist Security Info=True;User ID=sa;Password=123";

            // создание объекта подключения
            SqlConnection connection = new SqlConnection(connectionString);

            // Создание диалога для выбора пути сохранения резервной копии
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "SQL Server backup files (*.bak)|*.bak";
            saveFileDialog.FileName = "Backup_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".bak";
            if (saveFileDialog.ShowDialog() == true)
            {

                // Обновление backupCommandText с учетом выбранного пути сохранения
                string backupCommandText = $"BACKUP DATABASE [Brewery] TO DISK='{saveFileDialog.FileName}'";
                SqlCommand backupCommand = new SqlCommand(backupCommandText, connection);

                try
                {
                    // открытие соединения
                    connection.Open();

                    // выполнение команды резервного копирования
                    backupCommand.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
                finally
                {
                    // закрытие соединения
                    connection.Close();
                }
            }
        }
        public void SaveTableReg()
        {
            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey subKey = currentUser.CreateSubKey("PracticeTable");
            subKey.SetValue("OpenedTable", tabName);
            subKey.Close();
        }
        public void OpenTableReg()
        {
            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey subKey = currentUser.OpenSubKey("PracticeTable");
            if (subKey != null)
            {
                tabName = subKey.GetValue("OpenedTable").ToString();
                subKey.Close();
            }
        }
        private TabItem FindTabItem(ItemsControl itemsControl, string header)
        {
            // Найти TabItem с указанным заголовком (Header) внутри указанного ItemsControl
            return itemsControl.Items.OfType<TabItem>().FirstOrDefault(item => item.Header.Equals(header));
        }
        public void PlotData<T>(List<T> data, ScottPlot.WpfPlot plotControl)
        {
            double[] xValues;
            double[] yValues;
            xValues = data.Select(item => Convert.ToDouble(item.GetType().GetProperty("IdCheque").GetValue(item))).ToArray();
            yValues = data.Select(item => Convert.ToDouble(item.GetType().GetProperty("Sum").GetValue(item))).ToArray();
            plotControl.Plot.XLabel("ID");
            plotControl.Plot.YLabel("Сумма");
            plotControl.Plot.Title("График данных");
            // добавляем точки на существующий объект ScottPlot.WpfPlot
            plotControl.Plot.AddScatter(xValues, yValues);
            // обновляем отображение графика
            plotControl.Render();
        }

        //---------------------------------------------Commit---------------------------------------------------------
        private async void btCommitAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<Admin>(dgAdmin);
                PutTable<Admin>(dgAdmin);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitBrewery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<Breweries>(dgBrewery);
                PutTable<Breweries>(dgBrewery);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitSupplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<Supplier>(dgSupplier);
                PutTable<Supplier>(dgSupplier);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitIngridientsType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<IngridientsType>(dgIngridients);
                PutTable<IngridientsType>(dgIngridients);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitIngridients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<Ingridient>(dgIngridients);
                PutTable<Ingridient>(dgIngridients);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitBeerType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<BeerType>(dgBeerType);
                PutTable<BeerType>(dgBeerType);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitBeer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<Beer>(dgBeer);
                PutTable<Beer>(dgBeer);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitSubscription_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<Subscription>(dgSubscription);
                PutTable<Subscription>(dgSubscription);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<User>(dgUser);
                PutTable<User>(dgUser);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitIngridientsBeer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<IngridientsBeer>(dgIngridientsBeer);
                PutTable<IngridientsBeer>(dgBrewery_Beer);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitCheque_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<Cheque>(dgCheque);
                PutTable<Cheque>(dgCheque);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitBeer_Cheque_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<BeerCheque>(dgBeer_Cheque);
                PutTable<BeerCheque>(dgBeer_Cheque);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitBrewery_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<BreweryIngridient>(dgBrewery_Ingridients);
                PutTable<BreweryIngridient>(dgBrewery_Ingridients);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void btCommitBrewery_Beer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                CreateTable<BreweryBeer>(dgBrewery_Beer);
                PutTable<BreweryBeer>(dgBrewery_Beer);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //---------------------------------------------Delete---------------------------------------------------------
        private void btDeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<Admin>(dgAdmin);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteBrewery_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<Breweries>(dgBrewery);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<Supplier>(dgSupplier);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteIngridientsType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<IngridientsType>(dgIngridientsType);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteIngridients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<Ingridient>(dgIngridients);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteBeerType_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<BeerType>(dgBeerType);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteBeer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<Beer>(dgBeer);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteSubscription_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<Subscription>(dgSubscription);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<User>(dgUser);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteIngridientsBeer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<IngridientsBeer>(dgIngridientsBeer);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteCheque_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<Cheque>(dgCheque);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteBeer_Cheque_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<BeerCheque>(dgBeer_Cheque);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteBrewery_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<BreweryIngridient>(dgBrewery_Ingridients);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btDeleteBrewery_Beer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Создание экземпляра HttpClientHandler с обработчиком события ServerCertificateCustomValidation.
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };

                // Создание экземпляра HttpClient с настроенным HttpClientHandler.
                var httpClient = new HttpClient(handler);

                // Создание экземпляра DelegatingHandler с настроенным HttpClient.
                var messageHandler = new HttpClientHandler()
                {
                    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
                };


                // создаем подключение к хабу
                connection = new HubConnectionBuilder()
                    .WithUrl("https://172.20.10.2:7201/hub", options =>
                    {
                        options.HttpMessageHandlerFactory = _ => messageHandler;
                    })
                    .Build();

                DeleteTable<BreweryBeer>(dgBrewery_Beer);

                Update_Load();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //---------------------------------------------Exit---------------------------------------------------------
        private void btExitAdminList_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBreweryList_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitSuppliersList_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitIngridientsList_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBeerList_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitChequeList_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitUserList_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBrewery_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitAdmin_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitSupplier_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitIngridientsType_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitIngridients_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBeerType_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBeer_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitSubscription_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitUser_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitIngridientsBeer_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitCheque_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBeer_Cheque_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBrewery_Ingridients_Click(object sender, RoutedEventArgs e) => Exit();

        private void btExitBrewery_Beer_Click(object sender, RoutedEventArgs e) => Exit();


        //---------------------------------------------LogicalDelete---------------------------------------------------------
        private void btLogicalDeleteBrewery_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<Breweries>(dgBrewery);
            Update_Load();
        }

        private void btLogicalDeleteAdmin_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<Admin>(dgAdmin);
            Update_Load();
        }

        private void btLogicalDeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<Supplier>(dgSupplier);
            Update_Load();
        }

        private void btLogicalDeleteIngridientsType_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<IngridientsType>(dgIngridientsType);
            Update_Load();
        }

        private void btLogicalDeleteIngridients_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<Ingridient>(dgIngridients);
            Update_Load();
        }

        private void btLogicalDeleteBeerType_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<BeerType>(dgBeerType);
            Update_Load();
        }

        private void btLogicalDeleteBeer_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<Beer>(dgBeer);
            Update_Load();
        }

        private void btLogicalDeleteSubscription_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<Subscription>(dgSubscription);
            Update_Load();
        }

        private void btLogicalDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<User>(dgUser);
            Update_Load();
        }

        private void btLogicalDeleteIngridientsBeer_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<IngridientsBeer>(dgIngridientsBeer);
            Update_Load();
        }

        private void btLogicalDeleteCheque_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<Cheque>(dgCheque);
            Update_Load();
        }

        private void btLogicalDeleteBeer_Cheque_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<BeerCheque>(dgBeer_Cheque);
            Update_Load();
        }

        private void btLogicalDeleteBrewery_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<BreweryIngridient>(dgBrewery_Ingridients);
            Update_Load();
        }

        private void btLogicalDeleteBrewery_Beer_Click(object sender, RoutedEventArgs e)
        {
            LogicalDelete<BreweryBeer>(dgBrewery_Beer);
            Update_Load();
        }



        //---------------------------------------------LogicalRestore---------------------------------------------------------
        private void btLogicalRestoreBrewery_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<Breweries>(dgBrewery);
            Update_Load();
        }

        private void btLogicalRestoreAdmin_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<Admin>(dgAdmin);
            Update_Load();
        }

        private void btLogicalRestoreSupplier_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<Supplier>(dgSupplier);
            Update_Load();
        }

        private void btLogicalRestoreIngridientsType_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<IngridientsType>(dgIngridientsType);
            Update_Load();
        }

        private void btLogicalRestoreIngridients_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<IngridientsType>(dgIngridientsType);
            Update_Load();
        }

        private void btLogicalRestoreBeerType_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<BeerType>(dgBeerType);
            Update_Load();
        }

        private void btLogicalRestoreBeer_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<Beer>(dgBeer);
            Update_Load();
        }

        private void btLogicalRestoreSubscription_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<Subscription>(dgSubscription);
            Update_Load();
        }

        private void btLogicalRestoreUser_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<User>(dgUser);
            Update_Load();
        }

        private void btLogicalRestoreIngridientsBeer_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<IngridientsBeer>(dgIngridientsBeer);
            Update_Load();
        }

        private void btLogicalRestoreCheque_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<Cheque>(dgCheque);
            Update_Load();
        }

        private void btLogicalRestoreBeer_Cheque_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<BeerCheque>(dgBeer_Cheque);
            Update_Load();
        }

        private void btLogicalRestoreBrewery_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<BreweryIngridient>(dgBrewery_Ingridients);
            Update_Load();
        }

        private void btLogicalRestoreBrewery_Beer_Click(object sender, RoutedEventArgs e)
        {
            LogicalRestore<BreweryBeer>(dgBrewery_Beer);
            Update_Load();
        }



        //---------------------------------------------Export-To-Excel---------------------------------------------------------
        private void btExportToExcelBrewery_Click(object sender, RoutedEventArgs e)
        {
            List<Breweries> list = GetDataFromGrid<Breweries>(dgBrewery);
            ExportToExcel<Breweries>(list);
        }

        private void btExportToExcelAdmin_Click(object sender, RoutedEventArgs e)
        {
            List<Admin> list = GetDataFromGrid<Admin>(dgAdmin);
            ExportToExcel<Admin>(list);
        }

        private void btExportToExcelSupplier_Click(object sender, RoutedEventArgs e)
        {
            List<Supplier> list = GetDataFromGrid<Supplier>(dgSupplier);
            ExportToExcel<Supplier>(list);
        }

        private void btExportToExcelIngridientsType_Click(object sender, RoutedEventArgs e)
        {
            List<IngridientsType> list = GetDataFromGrid<IngridientsType>(dgIngridientsType);
            ExportToExcel<IngridientsType>(list);
        }

        private void btExportToExcelIngridients_Click(object sender, RoutedEventArgs e)
        {
            List<Ingridient> list = GetDataFromGrid<Ingridient>(dgIngridients);
            ExportToExcel<Ingridient>(list);
        }

        private void btExportToExcelBeerType_Click(object sender, RoutedEventArgs e)
        {
            List<Ingridient> list = GetDataFromGrid<Ingridient>(dgIngridients);
            ExportToExcel<Ingridient>(list);
        }

        private void btExportToExcelBeer_Click(object sender, RoutedEventArgs e)
        {
            List<Beer> list = GetDataFromGrid<Beer>(dgBeer);
            ExportToExcel<Beer>(list);
        }

        private void btExportToExcelSubscription_Click(object sender, RoutedEventArgs e)
        {
            List<Subscription> list = GetDataFromGrid<Subscription>(dgSubscription);
            ExportToExcel<Subscription>(list);
        }

        private void btExportToExcelUser_Click(object sender, RoutedEventArgs e)
        {
            List<User> list = GetDataFromGrid<User>(dgUser);
            ExportToExcel<User>(list);
        }

        private void btExportToExcelIngridientsBeer_Click(object sender, RoutedEventArgs e)
        {
            List<IngridientsBeer> list = GetDataFromGrid<IngridientsBeer>(dgIngridientsBeer);
            ExportToExcel<IngridientsBeer>(list);
        }

        private void btExportToExcelCheque_Click(object sender, RoutedEventArgs e)
        {
            List<Cheque> list = GetDataFromGrid<Cheque>(dgCheque);
            ExportToExcel<Cheque>(list);
        }

        private void btExportToExcelBeer_Cheque_Click(object sender, RoutedEventArgs e)
        {
            List<BeerCheque> list = GetDataFromGrid<BeerCheque>(dgBeer_Cheque);
            ExportToExcel<BeerCheque>(list);
        }

        private void btExportToExcelBrewery_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            List<BreweryIngridient> list = GetDataFromGrid<BreweryIngridient>(dgBrewery_Ingridients);
            ExportToExcel<BreweryIngridient>(list);
        }

        private void btExportToExcelBrewery_Beer_Click(object sender, RoutedEventArgs e)
        {
            List<BreweryBeer> list = GetDataFromGrid<BreweryBeer>(dgBrewery_Beer);
            ExportToExcel<BreweryBeer>(list);
        }



        //---------------------------------------------Export-To-Word---------------------------------------------------------
        private void btExportToWordBrewery_Click(object sender, RoutedEventArgs e)
        {
            List<Breweries> list = GetDataFromGrid<Breweries>(dgBrewery);
            ExportToWord<Breweries>(list);
        }

        private void btExportToWordAdmin_Click(object sender, RoutedEventArgs e)
        {
            List<Admin> list = GetDataFromGrid<Admin>(dgAdmin);
            ExportToWord<Admin>(list);
        }

        private void btExportToWordSupplier_Click(object sender, RoutedEventArgs e)
        {
            List<Supplier> list = GetDataFromGrid<Supplier>(dgSupplier);
            ExportToWord<Supplier>(list);
        }

        private void btExportToWordIngridientsType_Click(object sender, RoutedEventArgs e)
        {
            List<IngridientsType> list = GetDataFromGrid<IngridientsType>(dgIngridientsType);
            ExportToWord<IngridientsType>(list);
        }

        private void btExportToWordIngridients_Click(object sender, RoutedEventArgs e)
        {
            List<Ingridient> list = GetDataFromGrid<Ingridient>(dgIngridients);
            ExportToWord<Ingridient>(list);
        }

        private void btExportToWordBeerType_Click(object sender, RoutedEventArgs e)
        {
            List<BeerType> list = GetDataFromGrid<BeerType>(dgBeerType);
            ExportToWord<BeerType>(list);
        }

        private void btExportToWordBeer_Click(object sender, RoutedEventArgs e)
        {
            List<Beer> list = GetDataFromGrid<Beer>(dgBeer);
            ExportToWord<Beer>(list);
        }

        private void btExportToWordSubscription_Click(object sender, RoutedEventArgs e)
        {
            List<Subscription> list = GetDataFromGrid<Subscription>(dgSubscription);
            ExportToWord<Subscription>(list);
        }

        private void btExportToWordUser_Click(object sender, RoutedEventArgs e)
        {
            List<User> list = GetDataFromGrid<User>(dgUser);
            ExportToWord<User>(list);
        }

        private void btExportToWordIngridientsBeer_Click(object sender, RoutedEventArgs e)
        {
            List<IngridientsBeer> list = GetDataFromGrid<IngridientsBeer>(dgIngridientsBeer);
            ExportToWord<IngridientsBeer>(list);
        }

        private void btExportToWordCheque_Click(object sender, RoutedEventArgs e)
        {
            List<Cheque> list = GetDataFromGrid<Cheque>(dgCheque);
            ExportToWord<Cheque>(list);
        }

        private void btExportToWordBeer_Cheque_Click(object sender, RoutedEventArgs e)
        {
            List<BeerCheque> list = GetDataFromGrid<BeerCheque>(dgBeer_Cheque);
            ExportToWord<BeerCheque>(list);
        }

        private void btExportToWordBrewery_Ingridients_Click(object sender, RoutedEventArgs e)
        {
            List<BreweryIngridient> list = GetDataFromGrid<BreweryIngridient>(dgBrewery_Ingridients);
            ExportToWord<BreweryIngridient>(list);
        }

        private void btExportToWordBrewery_Beer_Click(object sender, RoutedEventArgs e)
        {
            List<BreweryBeer> list = GetDataFromGrid<BreweryBeer>(dgBrewery_Beer);
            ExportToWord<BreweryBeer>(list);
        }



        //---------------------------------------------Backup---------------------------------------------------------
        private void btCreateBackup_Click(object sender, RoutedEventArgs e)
        {
            CreateBackup();
        }



        //---------------------------------------------Graphs---------------------------------------------------------
        private async void DoGraph(object sender, RoutedEventArgs e)
        {
            ClearPlot(PlotGraph);
            List<Cheque> data = GetDataFromGrid<Cheque>(dgCheque); // получение данных из базы данных
            PlotData<Cheque>(data, PlotGraph); // генерация графика
        }
        public void ClearPlot(ScottPlot.WpfPlot plotControl)
        {
            plotControl.Reset();
        }
    }
}
