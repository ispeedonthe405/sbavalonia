using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;

namespace sbavalonia.controls;

public partial class OverlayTextBox : UserControl
{
    /////////////////////////////////////////////////////////
    #region Fields

    private static readonly Dictionary<Key, bool> NumberKeys = [];

    #endregion Fields
    /////////////////////////////////////////////////////////



    /////////////////////////////////////////////////////////
    #region Properties

    public static readonly StyledProperty<string?> TextProperty =
        AvaloniaProperty.Register<OverlayTextBox, string?>(nameof(Text), string.Empty);

    public static readonly StyledProperty<IBrush> TextColorProperty =
        AvaloniaProperty.Register<OverlayTextBox, IBrush>(nameof(TextColor), new SolidColorBrush(Colors.Black));

    public static readonly StyledProperty<string> OverlaySymbolProperty =
        AvaloniaProperty.Register<OverlayTextBox, string>(nameof(OverlaySymbol), "$");

    public static readonly StyledProperty<IBrush> OverlaySymbolColorProperty =
        AvaloniaProperty.Register<OverlayTextBox, IBrush>(nameof(OverlaySymbolColor), new SolidColorBrush(Colors.Black));

    public static readonly StyledProperty<bool> IsNumericProperty =
        AvaloniaProperty.Register<OverlayTextBox, bool>(nameof(IsNumeric), false);

    public string? Text
    {
        get => tb.Text;
        set
        {
            SetValue(TextProperty, value);
            tb.Text = value;
        }
    }

    public IBrush TextColor
    {
        get => GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public string OverlaySymbol
    {
        get => GetValue(OverlaySymbolProperty);
        set => SetValue(OverlaySymbolProperty, value);
    }

    public IBrush OverlaySymbolColor
    {
        get => GetValue(OverlaySymbolColorProperty);
        set => SetValue(OverlaySymbolColorProperty, value);
    }

    public bool IsNumeric
    {
        get => GetValue(IsNumericProperty);
        set => SetValue(IsNumericProperty, value);
    }

    #endregion Properties
    /////////////////////////////////////////////////////////



    /////////////////////////////////////////////////////////
    #region Interface

    public OverlayTextBox()
    {
        DataContext = this;
        InitializeComponent();

        KeyDown += OnKey;
        KeyUp += OnKey;
    }

    private void OnKey(object? sender, Avalonia.Input.KeyEventArgs e)
    {
        if (!IsNumeric) return;
        if(!NumberKeys.TryGetValue(e.Key, out var numberKey))
        {
            e.Handled = true;
        }
    }

    #endregion Interface
    /////////////////////////////////////////////////////////



    /////////////////////////////////////////////////////////
    #region Internal Affairs

    static OverlayTextBox()
    {
        NumberKeys.Add(Key.D0, true);
        NumberKeys.Add(Key.D1, true);
        NumberKeys.Add(Key.D2, true);
        NumberKeys.Add(Key.D3, true);
        NumberKeys.Add(Key.D4, true);
        NumberKeys.Add(Key.D5, true);
        NumberKeys.Add(Key.D6, true);
        NumberKeys.Add(Key.D7, true);
        NumberKeys.Add(Key.D8, true);
        NumberKeys.Add(Key.D9, true);

        NumberKeys.Add(Key.NumPad0, true);
        NumberKeys.Add(Key.NumPad1, true);
        NumberKeys.Add(Key.NumPad2, true);
        NumberKeys.Add(Key.NumPad3, true);
        NumberKeys.Add(Key.NumPad4, true);
        NumberKeys.Add(Key.NumPad5, true);
        NumberKeys.Add(Key.NumPad6, true);
        NumberKeys.Add(Key.NumPad7, true);
        NumberKeys.Add(Key.NumPad8, true);
        NumberKeys.Add(Key.NumPad9, true);

        NumberKeys.Add(Key.OemPeriod, true);
        NumberKeys.Add(Key.Decimal, true);

        NumberKeys.Add(Key.Return, true);
        NumberKeys.Add(Key.Back, true);
    }

    #endregion Internal Affairs
    /////////////////////////////////////////////////////////
}