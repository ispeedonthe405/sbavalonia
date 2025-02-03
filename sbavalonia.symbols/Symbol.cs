using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace sbavalonia.symbols
{
    public class Symbol : Image
    {
        /////////////////////////////////////////////////////////
        #region Fields

        private string _SymbolName = string.Empty;

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

        #endregion Properties
        /////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////
        #region Interface

        public Symbol() :
            base()
        {
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
        }

        #endregion Internal
        /////////////////////////////////////////////////////////        
	}
}
