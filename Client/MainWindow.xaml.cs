using Core;
using Core.Dtos;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Client
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<MessageDto> Messages { get; set; }

        private HubConnection _hubConnection;
        private string labelColor;

        public MainWindow()
        {
            InitializeComponent();

            this.labelColor = Generator.LabelColor();

            Messages = new ObservableCollection<MessageDto>();
            ListMessages.ItemsSource = Messages;

            _hubConnection = new HubConnectionBuilder()
                            .WithUrl("https://localhost:44321/chatHub")
                            .Build();

            _hubConnection.Closed += async (error) =>
            {
                await _hubConnection.StartAsync();
            };

            onReceiver();
        }

        private async void onReceiver()
        {
            _hubConnection.On<MessageDto>("messages", result =>
            {
                var formatMessage = string.Format("Username: {0} --> Message: {1}", result.Username, result.Message);
                Console.WriteLine(formatMessage);

                Messages.Add(result);
            });

            try
            {
                await _hubConnection.StartAsync();
                Messages.Add(new MessageDto { Username = "System", Message = "Bağlantı sağlandı...", Color = Generator.SystemColor });
            }
            catch (Exception ex)
            {
                Messages.Add(new MessageDto { Username = "System", Message = "Bağlantı sağlanamadı...", Color = Generator.SystemColor });
                //MessageBox.Show(ex.Message.ToString());
            }
        }

        private async void BtnSend_Click_Async(object sender, RoutedEventArgs e)
        {
            try
            {
                var messageDto = new MessageDto()
                {
                    Username = Environment.MachineName,
                    Message = TxtMessage.Text,
                    Color = labelColor
                };

                await _hubConnection.SendAsync("sendMessage", messageDto);
            }
            catch (Exception ex)
            {
                Messages.Add(new MessageDto { Username = "System", Message = "Bağlantı kesildi...", Color = Generator.SystemColor });
                //MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}