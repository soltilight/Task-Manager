using System.Diagnostics;
using System.Text;
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

namespace ProcessShowcase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer refreshTimer;
        public MainWindow()
        {

            InitializeComponent();
            GetAllProcesses();
            InitializeTimer();

        }


        private void showProcesses(ListBoxItem item)
        {
            string StartTime = "";
            string id = "";
            string PageMemorySize64 = "";
            string VirtualMemorySize64 = "";
            string Handle = "";
            Process process = (Process)item.Tag;
            try
            {
                Handle = process.Handle.ToString();
            }
            catch (Exception ex)
            {
                Handle = "Нет доступа";
            }
            try
            {
                id = process.Id.ToString();
            }
            catch (Exception ex)
            {
                id = "Нет доступа";
            }

            try
            {
                StartTime = process.StartTime.ToString();
            }
            catch (Exception ex)
            {
                StartTime = "Нет доступа";
            }

            try
            {
                PageMemorySize64 = process.PagedMemorySize64.ToString();
            }
            catch (Exception ex)
            {
                PageMemorySize64 = "Нет доступа";
            }

            try
            {
                VirtualMemorySize64 = process.VirtualMemorySize64.ToString();
            }
            catch (Exception ex)
            {
                VirtualMemorySize64 = "Нет доступа";
            }
            MessageBox.Show($"Handle-{Handle}\nId-{id}\nСтарт процесса в-{StartTime}\nОбъем памяти для процесса-{PageMemorySize64}\nОбъем виртуальной памяти для процесса-{VirtualMemorySize64}");
        }
        private void Process_Selection(object sender, SelectionChangedEventArgs e)
        {
            if (ProcessList.SelectedItem is ListBoxItem selectedItem)
            {
                showProcesses(selectedItem);
            }
        }
        private void InitializeTimer()
        {
            refreshTimer = new DispatcherTimer();
            refreshTimer.Interval = TimeSpan.FromSeconds(10);
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            GetAllProcesses();
        }
        private void GetAllProcesses()
        {
            ProcessList.Items.Clear();
            Process[] processes = Process.GetProcesses();
            for (int i = 0; i < processes.Length; i++)
            {
                ListBoxItem item = new ListBoxItem();
                try
                {
                    item.Content = $"{processes[i].ProcessName}";
                    item.Tag = processes[i];

                    ProcessList.Items.Add(item);
                }
                catch (Exception ex)
                {
                    item.Content = "Нет доступа к информации";
                }
            }
        }
    }
}
