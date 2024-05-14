using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using NAudio.Wave;
using System;
using System.Collections.ObjectModel;
using System.Media;
using System.Threading.Tasks;

namespace AnyaProject
{
    public partial class ProductsWindow1 : Window
    {
        private Border collapsiblePanel;
        private User queenUser;
        private string SoundPath;

        public ProductsWindow1()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ProductsWindow1(User QueenUser)
        {
            SoundPath = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\Rancho.wav";
            this.PlayMusic(SoundPath);

            InitializeComponent();
            DataContext = this;

            queenUser = QueenUser;

            Button dobavitTovar = this.FindControl<Button>("DobavitTovar");
            Button deleteVibrannoe = this.FindControl<Button>("DeleteVibrannoe");

            if (queenUser.UserStatus == "Queen")
            {
                dobavitTovar.IsVisible = true;
                deleteVibrannoe.IsVisible = true;
            }
            else
            {
                dobavitTovar.IsVisible = false;
                deleteVibrannoe.IsVisible = false;
            }

            this.Opened += ManagerUserWindow_WindowOpened;
        }

        private void ManagerUserWindow_WindowOpened(object? sender, EventArgs e)
        {
            // Установка значения свойства IsAdmin для каждого элемента в ProductsList
            foreach (var product in Products)
            {
                product.Otobrazhenie = queenUser.UserStatus == "Queen";
            }
        }

        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>()
        {
            new Product()
            {
                TovarImage = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Photos\Petlya.jpg",
                TovarName = "Петля для хлыста",
                Manufacturer = "EQUIMAN, Китай",
                Description = "Запасная петля для хлыста. Подходит для большинства хлыстов. Длина: 370 мм. Ширина: 12 мм.",
                Price = 135,
                Stock = 321
            }
        };

        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>()
        {
            new User()
            {
                UserName = "AnnaVedm",
                UserPassword = "1234",
                UserStatus = "Queen"
            },

            new User()
            {
                UserName = "Anna",
                UserPassword = "1234",
                UserStatus = "Thief"
            }
        };

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            collapsiblePanel = this.FindControl<Border>("CollapsiblePanel");
        }

        private void TogglePanel_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // Изменяем видимость панели
            collapsiblePanel.IsVisible = !collapsiblePanel.IsVisible;
        }

        public void RedactButton(object sender, RoutedEventArgs e)
        {
            Button edittovar = (Button)sender;

            Product editTovar = (Product)edittovar.DataContext;

            Redactirovanie tovar = new Redactirovanie(editTovar);
            tovar.Show();
        }

        private async void Exit_ButtonClick(object sender, RoutedEventArgs e) //кнопка перехода обратно на окно авторизации
        {
            SoundPath = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\Rancho.wav";
            this.StopSound(SoundPath);

            SoundPath = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\horseRzhanie.wav";
            this.PlayMusic(SoundPath);

            MainWindow window = new MainWindow();

            SoundPath = @"C:\Users\prdb\Desktop\AnyaProject-master\AnyaProject-master\Sounds\prologue.wav";
            window.PlaySound(SoundPath);
            window.Show();
            this.Close();
        }

        private async void PlayMusic(string SoundPath)
        {
            using (var audioFile = new AudioFileReader(SoundPath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                await Task.Delay(TimeSpan.FromSeconds(audioFile.TotalTime.TotalSeconds));
            }
        }

        private async void StopSound(string SoundPath)
        {
            SoundPlayer ss = new SoundPlayer(SoundPath);
            ss.Stop();
        }
    }
}

