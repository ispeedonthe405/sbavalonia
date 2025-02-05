using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;


namespace sbavalonia.symbols
{
    public class Symbol : Image
    {
        /////////////////////////////////////////////////////////
        #region Fields

        private string _SymbolName = string.Empty;
        private Color _OverrideColor = Colors.Transparent;

        #endregion Fields
        /////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////
        #region Properties

        public static readonly DirectProperty<Symbol, string> SymbolNameProperty =
            AvaloniaProperty.RegisterDirect<Symbol, string>(
                nameof(SymbolName),
                o => o.SymbolName,
                (o, v) => { o.SymbolName = v; });

        public string SymbolName
        {
            get => _SymbolName;
            set
            {
                SetAndRaise(SymbolNameProperty, ref _SymbolName, value);
            }
        }

        public static readonly DirectProperty<Symbol, Color> OverrideColorProperty =
            AvaloniaProperty.RegisterDirect<Symbol, Color>(
                nameof(OverrideColor),
                o => o.OverrideColor,
                (o, v) => { o.OverrideColor = v; });

        public Color OverrideColor
        {
            get => _OverrideColor;
            set
            {
                SetAndRaise(OverrideColorProperty, ref _OverrideColor, value);
            }
        }

        #endregion Properties
        /////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////
        #region Interface

        public Symbol() :
            base()
        {
            // defaults
            Width = 24; Height = 24;

            SymbolManager.LoadSymbol(SymbolName);
            ApplySourceToSymbol();
            PropertyChanged += Symbol_PropertyChanged;
            SymbolManager.SymbolColorChanged += SymbolManager_SymbolColorChanged;
        }

        public void ApplySourceToSymbol()
        {
            SymbolManager.Symbols.TryGetValue(SymbolName, out WriteableBitmap? bitmap);
            if (bitmap is not null)
            {
                if (OverrideColor != Colors.Transparent)
                {
                    SymbolManager.RecolorBitmap(ref bitmap, OverrideColor);
                }
                
                Source = bitmap;

                // In WPF this isn't necessary but it seems it is here in Avalonia
                InvalidateVisual();
            }
        }

        #endregion Interface
        /////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////
        #region Internal

        private void SymbolManager_SymbolColorChanged(object? sender, EventArgs e)
        {
            ApplySourceToSymbol();
        }

        private void Symbol_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals(nameof(SymbolName)))
            {
                SymbolManager.LoadSymbol(SymbolName);
                ApplySourceToSymbol();
            }

            else if (e.Property.Name.Equals(nameof(OverrideColor)))
            {
                ApplySourceToSymbol();
            }
        }

        #endregion Internal
        /////////////////////////////////////////////////////////        
	}
}
