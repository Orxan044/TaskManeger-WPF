using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Data;

namespace TaskManeger_WPF;

public partial class MainWindow : Window
{
    private ICollectionView? _filter;
    
    ObservableCollection<Process> processesList = new(Process.GetProcesses());
    
    
    ProcessStartInfo process = new();
    public MainWindow()
    { 

        InitializeComponent();
        ListView1.ItemsSource = processesList;
        SetupCollecitonView();
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
            processesList.Remove(select!);

            select!.Kill();

        }
    }

    private void SetupCollecitonView()
    {
        _filter = CollectionViewSource.GetDefaultView(processesList);
        _filter.Filter = FilterItems;
    }

    private void TxtBoxSearch_TextChanged(object obj, System.Windows.Controls.TextChangedEventArgs e)
    {
        _filter!.Refresh();
    }

    private bool FilterItems(object obj)
    {
        try
        {
            if (obj is Process p)
            {
                return string.IsNullOrEmpty(TxtBoxSearch.Text) || p.ProcessName!.Contains(TxtBoxSearch.Text, StringComparison.OrdinalIgnoreCase) ||
                p.Id.ToString()!.Contains(TxtBoxSearch.Text, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }
        catch (Exception ex)
        {

            MessageBox.Show(ex.Message);
            return false;
        }

    }

}