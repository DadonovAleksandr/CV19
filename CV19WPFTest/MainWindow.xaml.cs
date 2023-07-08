using System;
using System.Threading;
using System.Windows;

namespace CV19WPFTest;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        new Thread(ComputeValue).Start();
    }

    private void ComputeValue()
    {
        var value = LongProcess(DateTime.Now);

        if (Result.Dispatcher.CheckAccess())
            Result.Text = value;
        else
            //Result.Dispatcher.Invoke(() => Result.Text = value);
            Result.Dispatcher.BeginInvoke(new Action(() => Result.Text = value));
    }

    private static string LongProcess(DateTime time)
    {
        Thread.Sleep(10_000);

        return $"Value: {time}";
    }

    private void Ellipse_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {

    }
}
