using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;

namespace DummyActivityApp;
public class BackgroundText : Adorner
{
    private readonly TextBlock _placeholderTextBlock;

    public BackgroundText(UIElement adornedElement, string placeholderText) : base(adornedElement)
    {
        IsHitTestVisible = false; // 入力イベントをブロックしない

        _placeholderTextBlock = new TextBlock
        {
            Text = placeholderText,
            Foreground = Brushes.Gray,
            Margin = new Thickness(2, 0, 0, 0),
            VerticalAlignment = VerticalAlignment.Center,
            FontSize = 35
        };
    }

    protected override void OnRender(DrawingContext drawingContext)
    {
        var textBox = AdornedElement as TextBox;
        if (textBox == null || !string.IsNullOrEmpty(textBox.Text)) return;

        var formattedText = new FormattedText(
            _placeholderTextBlock.Text,
            System.Globalization.CultureInfo.CurrentUICulture,
            FlowDirection.LeftToRight,
            new Typeface(
                _placeholderTextBlock.FontFamily,
                _placeholderTextBlock.FontStyle,
                _placeholderTextBlock.FontWeight,
                _placeholderTextBlock.FontStretch),
            _placeholderTextBlock.FontSize,
            _placeholderTextBlock.Foreground,
            VisualTreeHelper.GetDpi(this).PixelsPerDip  // これが必要な場合もある
        );

        drawingContext.DrawText(formattedText, new Point(5, (textBox.RenderSize.Height - formattedText.Height) / 2));
    }
}