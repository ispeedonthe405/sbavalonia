using Avalonia;
using Avalonia.Media;

namespace sbavalonia.symbols
{
    public static class SymbolManager
    {
        private static Application? _App;
        public static event EventHandler? SymbolColorChanged;
        private static Color _Symbols_LightTheme = new(255, 31, 31, 31);
        private static Color _Symbols_DarkTheme = new(255, 227, 227, 227);


        /// <summary>
        /// This can be changed at any time
        /// </summary>
        public static Color LightThemeColor 
        {
            get => _Symbols_LightTheme;
            set { _Symbols_LightTheme = value; UpdateColor(); OnSymbolColorChanged(); }
        }

        
        /// <summary>
        /// This can be changed at any time
        /// </summary>
        public static Color DarkThemeColor
        {
            get => _Symbols_DarkTheme;
            set { _Symbols_DarkTheme = value; UpdateColor(); OnSymbolColorChanged(); }
        }

        public static void OnSymbolColorChanged()
        {
            SymbolColorChanged?.Invoke(null, EventArgs.Empty);
        }

        private static Color _SymbolColor = _Symbols_LightTheme;
        public static Color SymbolColor
        {
            get => _SymbolColor;
            private set
            {
                if (value != _SymbolColor)
                {
                    _SymbolColor = value;
                    OnSymbolColorChanged();
                }
            }
        }

        /// <summary>
        /// This function initializes the SymbolManager. Call this early in the 
        /// Application class lifetime, such as within the overridden 
        /// Application.Initialize().
        /// </summary>
        /// <param name="app"></param>
        /// <param name="lightThemeColor"></param>
        /// <param name="darkThemeColor"></param>
        public static void Integrate(Application app, Color? lightThemeColor = null, Color? darkThemeColor = null)
        {
            _App = app;
            _App.ActualThemeVariantChanged += App_ActualThemeVariantChanged;

            if (lightThemeColor is not null)
            {
                LightThemeColor = lightThemeColor.Value;
            }
            if (darkThemeColor is not null)
            {
                DarkThemeColor = darkThemeColor.Value;
            }

            UpdateColor();
        }

        private static void App_ActualThemeVariantChanged(object? sender, EventArgs e)
        {
            UpdateColor();
        }

        private static void UpdateColor()
        {
            if (_App!.ActualThemeVariant.Equals(Avalonia.Styling.ThemeVariant.Light))
            {
                SymbolColor = LightThemeColor;
            }
            else
            {
                SymbolColor = DarkThemeColor;
            }
        }
    }
}
