using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
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
                    RecolorBitmap(bitmap);
                    Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                sbdotnet.Logger.Error(ex);
            }
        }

        private unsafe void RecolorBitmap(WriteableBitmap bitmap)
        {
            try
            {
                using ILockedFramebuffer buffer = bitmap.Lock();
                var bytesPerPixel = 4;
                var pixelptr = (byte*)buffer.Address;
                int pixelCountMax = bitmap.PixelSize.Width * bitmap.PixelSize.Height;
                Color newColor = OverrideColor != Colors.Transparent ? OverrideColor : SymbolManager.SymbolColor;

                for (int pixelCurrent = 0; pixelCurrent < pixelCountMax; pixelCurrent++)
                {
                    var pixel = new Span<byte>(pixelptr + (pixelCurrent * bytesPerPixel), bytesPerPixel);

                    PixelFormat format = buffer.Format;
                    if (format == PixelFormat.Rgba8888)
                    {
                        if (pixel[3] == 0) continue;

                        pixel[0] = newColor.R;
                        pixel[1] = newColor.G;
                        pixel[2] = newColor.B;
                        pixel[3] = newColor.A;
                    }
                    else if (format == PixelFormat.Bgra8888)
                    {
                        if (pixel[3] == 0) continue;

                        pixel[0] = newColor.B;
                        pixel[1] = newColor.G;
                        pixel[2] = newColor.R;
                        pixel[3] = newColor.A;
                    }
                    else
                    {
                        sbdotnet.Logger.Warning($"PixelFormat {buffer.Format} is not supported");
                    }
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
