using Avalonia;
using Avalonia.Media;

namespace sbavalonia.symbols
{
    public static class SymbolManager
    {
        private static Application? _App;
        public static event EventHandler? SymbolColorChanged;
        private static Color _Symbol_Light = new(255, 227, 227, 227);
        private static Color _Symbol_Dark = new(255, 31, 31, 31);

        public static void OnSymbolColorChanged()
        {
            SymbolColorChanged?.Invoke(null, EventArgs.Empty);
        }

        private static Color _SymbolColor = _Symbol_Dark;
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

        public static void Integrate(Application app)
        {
            _App = app;
            _App.ActualThemeVariantChanged += App_ActualThemeVariantChanged;
        }

        private static void App_ActualThemeVariantChanged(object? sender, EventArgs e)
        {
            if(_App!.ActualThemeVariant.Equals(Avalonia.Styling.ThemeVariant.Light))
            {
                SymbolColor = _Symbol_Dark;
            }
            else
            {
                SymbolColor = _Symbol_Light;
            }
        }
    }
}
