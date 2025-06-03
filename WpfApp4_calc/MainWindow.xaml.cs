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

namespace WpfApp4_calc;

enum Operation { None, Add, Subtract, Multiply, Divide }
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Operation lastOperation = Operation.None;
    private double lastNumber;
    private string decimalDot = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;


    // propdp, TAB, TAB
    public string DisplayText
    {
        get { return (string)GetValue(DisplayTextProperty); }
        set { SetValue(DisplayTextProperty, value); }
    }

    // Using a DependencyProperty as the backing store for DisplayText.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty DisplayTextProperty =
        DependencyProperty.Register("DisplayText", typeof(string), typeof(MainWindow), new PropertyMetadata(""));



    public MainWindow()
    {
        InitializeComponent();
        DisplayText = "0";
    }

    private void NumBtnClick(object sender, RoutedEventArgs e)
    {
        string digit = ((Button)sender).Content.ToString();

        if (DisplayText == "0")
            DisplayText = digit;
        else
            DisplayText += digit;
    }

    private void DotBtn_Click(object sender, RoutedEventArgs e)
    {
        if (!DisplayText.Contains(decimalDot))
            DisplayText += decimalDot;
    }

    private void ACBtn_Click(object sender, RoutedEventArgs e)
    {
        DisplayText = "0";
    }

    private void PlusMinusBtn_Click(object sender, RoutedEventArgs e)
    {
        DisplayText = (double.Parse(DisplayText) * (-1)).ToString();
    }

    private void PercentBtn_Click(object sender, RoutedEventArgs e)
    {
        DisplayText = (double.Parse(DisplayText) * (100)).ToString();
    }

    private void OperationBtn_Click(object sender, RoutedEventArgs e)
    {
        string symbol = (sender as Button).Content.ToString();
        Operation operation = symbol switch
        {
            "+" => Operation.Add,
            "-" => Operation.Subtract,
            "x" => Operation.Multiply,
            "/" => Operation.Divide,
            _ => Operation.None
        };

        if (lastOperation != Operation.None)
        {
            double currentNum = double.Parse(DisplayText);
            lastNumber = Calculate(currentNum);
        } else
        {
            lastOperation = operation;
        }

        lastNumber = double.Parse(DisplayText);
        DisplayText = "0";
    }

    private void EqualsBtn_Click(object sender, RoutedEventArgs e)
    {
        double currentNum = double.Parse(DisplayText);
        DisplayText = Calculate(currentNum).ToString();
    }

    private double Calculate(double currentNum)
    {
        return lastOperation switch
        {
            Operation.Add => SimpleMath.Add(lastNumber, currentNum),
            Operation.Subtract => SimpleMath.Subtract(lastNumber, currentNum),
            Operation.Divide => SimpleMath.Divide(lastNumber, currentNum),
            Operation.Multiply => SimpleMath.Multiply(lastNumber, currentNum),
            _ => 0
        };
    }
}