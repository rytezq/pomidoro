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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace pomidoro
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DispatcherTimer timer;
        private TimeSpan currentTime;
        private bool isWorkPeriod = true;
        private const int work_time = 25;
        private const int break_time = 5;
        public MainWindow()
        {
            InitializeComponent();
            InitializeTimer(); 
            ResetTimer();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (currentTime.TotalSeconds > 0)
            {
                currentTime = currentTime.Subtract(TimeSpan.FromSeconds(1));
                UpdateTimeDisplay();
            }
            else
            {
                timer.Stop();
                if (isWorkPeriod)
                {
                    MessageBox.Show("Время отдыха!", "Техника Помидора", MessageBoxButton.OK, MessageBoxImage.Information);
                    StatusText.Text = "Время отдыха";
                    isWorkPeriod = false;
                    currentTime = TimeSpan.FromSeconds(break_time);
                    UpdateTimeDisplay();
                    Start();
                }
                else
                {
                    MessageBox.Show("Время работать!", "Техника Помидора", MessageBoxButton.OK, MessageBoxImage.Information);
                    StatusText.Text = "Время работать";
                    isWorkPeriod = true;
                    currentTime = TimeSpan.FromSeconds(work_time);
                    UpdateTimeDisplay();
                    Start();
                }
            }
        }

        private void Start()
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                StartB.Content = "Пауза";
                StartB.Background = Brushes.Violet;
            }
            else
            {
                timer.Stop();
                StartB.Content = "Старт";
                StartB.Background = Brushes.Purple;
            }
        }

        private void ResetTimer()
        {
            timer.Stop();
            isWorkPeriod = true;
            currentTime = TimeSpan.FromSeconds(work_time);
            UpdateTimeDisplay();
            StartB.Content = "Старт";
            StartB.Background = Brushes.Purple;
            StatusText.Text = "Готов к работе";
        }


        private void StartTimer()
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                StartB.Content = "Пауза";
                StartB.Background = Brushes.Violet;
            }
        }

        private void PauseTimer()
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                StartB.Content = "Старт";
                StartB.Background = Brushes.Purple;
            }
        }
        private void ToggleTimer()
        {
            if (timer.IsEnabled)
            {
                PauseTimer();
            }
            else
            {
                StartTimer();
            }
        }
        private void UpdateTimeDisplay()
        {
            Timer.Text = currentTime.ToString(@"mm\:ss");
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            ToggleTimer();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }
    }
}
