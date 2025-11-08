using System.Diagnostics;
using System.IO;
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

namespace DummyActivityApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public string WindowTitle = "FakeNameAppCreator";
    public string ICOdir = @"D:/FANC_Assets/ICOs";
    public string ICOpath = String.Empty;
    public MainWindow()
    {
        InitializeComponent();
        this.Loaded += (s, e) => this.Title = WindowTitle;
        this.Loaded += (s, e) => AddPlaceholder(applicationNameInput,"ここにアプリ名を入力・・・");
        this.Loaded += (s, e) => this.ICOdirInput.Text = ICOdir;
    }

    private BackgroundText _placeholderAdorner;

    private void AddPlaceholder(TextBox textBox, string placeholder)
    {
        var layer = AdornerLayer.GetAdornerLayer(textBox);
        if (layer != null)
        {
            _placeholderAdorner = new BackgroundText(textBox, placeholder);
            layer.Add(_placeholderAdorner);

            textBox.TextChanged += (s, e) =>
            {
                _placeholderAdorner.Visibility = string.IsNullOrEmpty(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            };

            // 初期表示制御
            _placeholderAdorner.Visibility = string.IsNullOrEmpty(textBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        if (textBox != null) { 
            this.WindowTitle = textBox.Text;
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        this.Title = WindowTitle;
        ICOpath = ICOdir + "/" + this.WindowTitle + ".ico";
        if (File.Exists(ICOpath))
        {
            this.Icon = new BitmapImage(new Uri(ICOpath, UriKind.RelativeOrAbsolute));
            Debug.WriteLine("ICO set called. path: " + ICOpath);
        }
        else
        {
            Debug.WriteLine("ICO set failed. path: "+ICOpath);
        }
    }

    private void saveICOdir(object sender, TextChangedEventArgs ev)
    {
        var textBox = sender as TextBox;
        if (textBox != null) { 
            ICOdir = textBox.Text;
        }
    }

    private void OpenDirectry(object sender, RoutedEventArgs ev)
    {
        if (!Directory.Exists(ICOdir))
        {
            Directory.CreateDirectory(ICOdir);
        }
        var psi = new ProcessStartInfo()
        {
            FileName = ICOdir,
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}