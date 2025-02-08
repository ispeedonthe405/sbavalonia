using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using sbdotnet;
using System.Reflection;


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

            PropertyChanged += Symbol_PropertyChanged;
            SymbolManager.SymbolColorChanged += SymbolManager_SymbolColorChanged;
        }

        #endregion Interface
        /////////////////////////////////////////////////////////



        /////////////////////////////////////////////////////////
        #region Internal

        private void SymbolManager_SymbolColorChanged(object? sender, EventArgs e)
        {
            if (!SymbolName.IsNull())
            {
                LoadSymbol();
            }            
        }

        private void Symbol_PropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.Property.Name.Equals(nameof(SymbolName)))
            {
                LoadSymbol();
            }

            else if (e.Property.Name.Equals(nameof(OverrideColor)))
            {
                LoadSymbol();
            }
        }

        private void LoadSymbol()
        {
            try
            {
                string prefix = "sbavalonia.symbols.symbols.";
                string suffix = ".png";
                string resourceName = $"{prefix}{SymbolName}{suffix}";

                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
                {
                    if (resource is null)
                    {
                        sbdotnet.Logger.Warning($"Failed to load symbol {SymbolName}");
                        return;
                    }

                    WriteableBitmap bitmap = WriteableBitmap.Decode(resource);

                    Color newColor = SymbolManager.SymbolColor;
                    if(OverrideColor != Colors.Transparent)
                    {
                        newColor = OverrideColor;
                    }
                    sbavalonia.media.ImageUtil.RecolorMonochromeBitmap(ref bitmap, newColor);
                    
                    Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                sbdotnet.Logger.Error(ex);
            }
        }

        #endregion Internal
        /////////////////////////////////////////////////////////        
    }
}
