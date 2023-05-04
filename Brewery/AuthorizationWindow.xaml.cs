using BreweryAPI.Models;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Brewery
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public string Token = "";
        HubConnection connection;  // подключение для взаимодействия с хабом

        public AuthorizationWindow()
        {
            InitializeComponent();

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

            connection.StartAsync();

            connection.On<string>("auth", (token) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (token != null)
                    {
                        AdminWindow adminWindow = new AdminWindow(token);
                        adminWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        tbError.Text = "Uncorrect values.";
                    }
                });
            });

            connection.On<string>("authWithKey", (token) =>
            {
                Dispatcher.Invoke(() =>
                {
                    if (token != null)
                    {
                        try
                        {
                            Token = token;

                            MessageBoxResult result = MessageBox.Show("You have an authorized user. Log in?", "Fast Authorization", MessageBoxButton.YesNo, MessageBoxImage.Question);

                            if (result == MessageBoxResult.Yes)
                            {
                                // пользователь нажал "Да"
                                AdminWindow window2 = new AdminWindow(Token);
                                window2.Show();
                                Close();
                            }
                            else
                            {
                                // пользователь нажал "Нет"
                                // не делаем ничего
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Can't get the key values.");
                    }
                });
            });
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private async void btAuth_Click(object sender, RoutedEventArgs e)
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

            string login = tbxLogin.Text;
            string password = tbxPassword.Text;
            try
            {
                if (Regist.IsChecked == true)
                {
                    // отправка сообщения
                    connection.InvokeAsync("GetAuthKeyAdmin", login);

                    connection.On<string>("getKey", (authKey) =>
                    {
                        Dispatcher.Invoke(() =>
                        {
                            if (authKey != null)
                            {
                                try
                                {
                                    // сохраняем данные в реестре
                                    RegistryKey currentUser = Registry.CurrentUser;
                                    RegistryKey subKey = currentUser.CreateSubKey("Practice");
                                    subKey.SetValue("Login", login);
                                    subKey.SetValue("AuthKey", authKey);
                                    subKey.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }

                            }
                            else
                            {
                                MessageBox.Show("Can't get the key values.");
                            }
                        });
                    });
                }
                // отправка сообщения
                await connection.InvokeAsync("AuthorizationAdmin", tbxLogin.Text, tbxPassword.Text);

            }
            catch (Exception ex)
            {
                tbError.Text = ex.Message;
            }


        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
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

                string login = null;
                string authKey = null;

                RegistryKey currentUser = Registry.CurrentUser;
                RegistryKey subKey = currentUser.OpenSubKey("Practice");
                if (subKey != null)
                {
                    login = subKey.GetValue("Login", null) as string;
                    authKey = subKey.GetValue("AuthKey", null) as string;
                    subKey.Close();
                }
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(authKey))
                {
                    return;
                }

                // отправка сообщения
                await connection.InvokeAsync("AuthorizationWithKeyAdmin", login, authKey);

                

                if (!string.IsNullOrEmpty(Token))
                {
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private async Task<string> GetAuthKeyAsync(string login)
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

                // отправка сообщения
                await connection.InvokeAsync("GetAuthKeyAdmin", login);


                connection.On<string>("getKey", (value) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (value != null)
                        {
                            try
                            {
                                return value;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return null;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Can't get the key values.");
                            return null;
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            return null;
        }
        private async Task<string> AUTH_key(string login, string key)
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

                // отправка сообщения
                await connection.InvokeAsync("AuthorizationWithKeyAdmin", login, key);

                connection.On<string>("authWithKey", (value) =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (value != null)
                        {
                            try
                            {
                                return value;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                return null;
                            }

                        }
                        else
                        {
                            MessageBox.Show("Can't get the key values.");
                            return null;
                        }
                    });
                });
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        private async void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                await connection.StopAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
