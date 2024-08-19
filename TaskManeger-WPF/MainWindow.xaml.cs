using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;

namespace TaskManeger_WPF;

public partial class MainWindow : Window
{


    ObservableCollection<Process> processesList = new(Process.GetProcesses());
    
    
    ProcessStartInfo process = new();
    public MainWindow()
    {
        InitializeComponent();
        ListView1.ItemsSource = processesList;
    }

    private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string text = txtBox.Text;
        
        try
        {
            if(text is not null)
            {
                process.FileName = text;
                var p = Process.Start(process);
                processesList.Add(p!);

            }
        }
        catch (Exception  ex)
        {
            MessageBox.Show(ex.ToString());
        }
        
    }
    
    private void Button_Click_1(object sender, RoutedEventArgs e)
    {

        if(ListView1.SelectedItem is not null)
        {
            var select = ListView1.SelectedItem as Process;
            //ListView1.Items.Remove(select);
            processesList.Remove(select!);

            select!.Kill();

        }
    }

    private void ListView1_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {

    }
}