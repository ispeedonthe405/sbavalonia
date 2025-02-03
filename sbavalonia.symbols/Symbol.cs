using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;

namespace sbavalonia.symbols
{
    public class Symbol : Image
    {
        public static readonly DirectProperty<Symbol, string> SymbolNameProperty =
            AvaloniaProperty.RegisterDirect<Symbol, string>(
                nameof(SymbolName),
                o => o.SymbolName,
                (o, v) => { o.SymbolName = v; });

        private string _SymbolName = string.Empty;

        public string SymbolName
        {
            get => _SymbolName;
            set
            {
                SetAndRaise(SymbolNameProperty, ref _SymbolName, value);
            }
        }

		
        public Symbol() :
            base()
        {
            ApplySourceToSymbol();
            PropertyChanged += Symbol_PropertyChanged;
            SymbolManager.SymbolColorChanged += SymbolManager_SymbolColorChanged;
        }

        private void SymbolManager_SymbolColorChanged(object? sender, EventArgs e)
        {
            sbdotnet.Logger.Notify("SymbolManager_SymbolColorChanged");
            ApplySourceToSymbol();
        }

        private void Symbol_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals(nameof(SymbolName)))
            {
                ApplySourceToSymbol();
            }
        }

        public void ApplySourceToSymbol()
        {
            SymbolManager.Symbols.TryGetValue(SymbolName, out WriteableBitmap? bitmap);
            if(bitmap is not null)
            {
                Source = bitmap;

                // In WPF this isn't necessary but it is here in Avalonia
                InvalidateVisual();
            }
        }
	}
}
