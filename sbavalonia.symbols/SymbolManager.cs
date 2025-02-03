using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Reflection;

namespace sbavalonia.symbols
{
    public static class SymbolManager
    {
        private static Color _SymbolColor = Colors.Ivory;
        public static Color SymbolColor
        {
            get => _SymbolColor;
            set
            {
                if (value != _SymbolColor)
                {
                    _SymbolColor = value;
                    RecolorSymbols();
                }
            }
        }
        public static Dictionary<string, WriteableBitmap> Symbols { get; private set; } = [];

        public static void Initialize()
        {
            BuildSymbols();
        }

        private static void BuildSymbols()
        {
            string prefix = "sbavalonia.symbols.symbols.";
            string suffix = ".png";
            var symbolResources = sbdotnet.AssemblyUtils.GetResourceNames(prefix, suffix);
            foreach (var resource in symbolResources)
            {
                using (var assemblyResource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                {
                    if (assemblyResource is not null)
                    {
                        WriteableBitmap value = WriteableBitmap.Decode(assemblyResource);
                        string key = resource.Replace(prefix, "").Replace(suffix, "");
                        Symbols.Add(key, value);
                    }
                }
            }
        }

        public static unsafe void RecolorSymbols()
        {
            sbdotnet.Logger.Notify("RecolorSymbols()");
            foreach (var symbol in Symbols.Values)
            {
                using ILockedFramebuffer buffer = symbol.Lock();
                var bytesPerPixel = 4;// buffer.Format.BitsPerPixel / sizeof(byte);
                var pixelptr = (byte*)buffer.Address;
                int pixelCountMax = symbol.PixelSize.Width * symbol.PixelSize.Height;

                for(int pixelCurrent = 0;  pixelCurrent < pixelCountMax; pixelCurrent++)
                {
                    var pixel = new Span<byte>(pixelptr + (pixelCurrent*4), bytesPerPixel);

                    Color currentColor = new Color(pixel[3], pixel[2], pixel[1], pixel[0]);
                    if (currentColor.A == 0) continue;

                    PixelFormat format = buffer.Format;
                    if (format == PixelFormat.Rgb565)
                    {
                        var value = (((_SymbolColor.R & 0b11111000) << 8) + ((_SymbolColor.G & 0b11111100) << 3) + (_SymbolColor.B >> 3));
                        pixel[0] = (byte)value;
                        pixel[1] = (byte)(value >> 8);
                    }
                    else if(format == PixelFormat.Rgba8888)
                    {
                        pixel[0] = _SymbolColor.R;
                        pixel[1] = _SymbolColor.G;
                        pixel[2] = _SymbolColor.B;
                        pixel[3] = _SymbolColor.A;
                    }
                    else if(format == PixelFormat.Bgra8888)
                    {
                        pixel[0] = _SymbolColor.B;
                        pixel[1] = _SymbolColor.G;
                        pixel[2] = _SymbolColor.R;
                        pixel[3] = _SymbolColor.A;
                    }
                    else
                    {
                        sbdotnet.Logger.Warning($"PixelFormat={buffer.Format}");
                    }
                }
            }
        }
    }
}
