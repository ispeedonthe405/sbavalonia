using Avalonia.Media;

namespace sbavalonia.symbols
{
    public static class SymbolManager
    {
        public static event EventHandler? SymbolColorChanged;

        public static void OnSymbolColorChanged()
        {
            SymbolColorChanged?.Invoke(null, EventArgs.Empty);
        }

        private static Color _SymbolColor = Colors.Ivory;
        public static Color SymbolColor
        {
            get => _SymbolColor;
            set
            {
                if (value != _SymbolColor)
                {
                    _SymbolColor = value;
                    OnSymbolColorChanged();
                }
            }
        }
    }
}
