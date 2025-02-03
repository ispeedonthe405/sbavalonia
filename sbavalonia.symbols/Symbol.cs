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
                ApplySourceToSymbol();
            }
        }

		
        public Symbol() :
            base()
        {
            ApplySourceToSymbol();
        }

        public void ApplySourceToSymbol()
        {
            SymbolManager.Symbols.TryGetValue(SymbolName, out WriteableBitmap? bitmap);
            if(bitmap is not null)
            {
                Source = bitmap;
            }
        }
	}
}
