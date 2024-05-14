using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using NAudio;
using NAudio.Wave;
using System.Threading.Tasks;

namespace AnyaProject
{
    public partial class MainWindow : Window
    {
        private string SoundPath = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\prologue.wav";

        User user = new User();

        public MainWindow()
        {
            //SoundPath = @"C:\Users\dlyau\OneDrive\Desktop\AnyaProject\Sounds\prologue.wav";
            PlaySound(SoundPath);

            InitializeComponent();
            DataContext = this;
        }


        private async void VoitiVAkkaynt(object sender, RoutedEventArgs e)
        {
            ProductsWindow1 w = new ProductsWindow1();

            if (!string.IsNullOrWhiteSpace(Name.Text)) //Если строка непустая, то проверяем наш список на наличие этих полей
            {
                User proverka = w.Users.FirstOrDefault(u => u.UserName == Name.Text && u.UserPassword == Password.Text);

                if (proverka != null)
                {
                    SoundPath = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\prologue.wav";
                    StopSound(SoundPath);

                    string path = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\horseRzhanie.wav";
                    PlaySound(path);

                    // Вход выполнен успешно
                    Oshibka.Text = null;

                    ProductsWindow1 wp = new ProductsWindow1(proverka);
                    wp.Show();

                    this.Close();
                    
                }
                else
                {
                    string path = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\horseRzhanie.wav";
                    PlaySound(path);

                    // Ошибка входа
                    Oshibka.Text = "Неверно введены данные";
                    Password.Clear();
                }
            }

            else
            {
                string path = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\horseRzhanie.wav";
                PlaySound(path);
            }
        }

        public async void PlaySound(string SoundPath)
        {
            using (var audioFile = new AudioFileReader(SoundPath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                await Task.Delay(TimeSpan.FromSeconds(audioFile.TotalTime.TotalSeconds));
            }

            //SoundPlayer ss = new SoundPlayer(SoundPath);
            //ss.Play();
        }

        private void StopSound(string SoundPath)
        {
            SoundPlayer ss = new SoundPlayer(SoundPath);
            ss.Stop();
        }
    }
}