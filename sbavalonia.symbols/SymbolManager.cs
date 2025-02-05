using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Reflection;

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
                    RecolorSymbols();
                }
            }
        }
        public static Dictionary<string, WriteableBitmap> Symbols { get; private set; } = [];

        public static void LoadSymbol(string symbol)
        {
            string prefix = "sbavalonia.symbols.symbols.";
            string suffix = ".png";
            string resourceName = $"{prefix}{symbol}{suffix}";

            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                if(resource is null)
                {
                    sbdotnet.Logger.Warning($"Failed to load symbol {symbol}");
                    return;
                }

                WriteableBitmap bitmap = WriteableBitmap.Decode(resource);
                RecolorBitmap(ref bitmap);
                
                if (Symbols.Keys.Contains(symbol))
                {
                    Symbols.Remove(symbol);
                }
                Symbols.Add(symbol, bitmap);
                
                OnSymbolColorChanged();
            }
        }

        private static void BuildAllSymbols()
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

        public static void OverrideSymbolColor(string symbolName, Color color)
        {
            if (Symbols.TryGetValue(symbolName, out WriteableBitmap? bitmap))
            {
                RecolorBitmap(ref bitmap, color);
            }
        }

        public static unsafe void RecolorBitmap(ref WriteableBitmap bitmap, Color? color = null)
        {
            using ILockedFramebuffer buffer = bitmap.Lock();
            var bytesPerPixel = 4;
            var pixelptr = (byte*)buffer.Address;
            int pixelCountMax = bitmap.PixelSize.Width * bitmap.PixelSize.Height;
            Color newColor = color is null ? SymbolColor : color.Value;

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

        public static unsafe void RecolorSymbols()
        {
            foreach (var symbol in Symbols)
            {
                using ILockedFramebuffer buffer = symbol.Value.Lock();
                var bytesPerPixel = 4;
                var pixelptr = (byte*)buffer.Address;
                int pixelCountMax = symbol.Value.PixelSize.Width * symbol.Value.PixelSize.Height;

                for(int pixelCurrent = 0;  pixelCurrent < pixelCountMax; pixelCurrent++)
                {
                    var pixel = new Span<byte>(pixelptr + (pixelCurrent * bytesPerPixel), bytesPerPixel);

                    PixelFormat format = buffer.Format;
                    if(format == PixelFormat.Rgba8888)
                    {
                        if (pixel[3] == 0) continue;

                        pixel[0] = _SymbolColor.R;
                        pixel[1] = _SymbolColor.G;
                        pixel[2] = _SymbolColor.B;
                        pixel[3] = _SymbolColor.A;
                    }
                    else if(format == PixelFormat.Bgra8888)
                    {
                        if (pixel[3] == 0) continue;

                        pixel[0] = _SymbolColor.B;
                        pixel[1] = _SymbolColor.G;
                        pixel[2] = _SymbolColor.R;
                        pixel[3] = _SymbolColor.A;
                    }
                    else
                    {
                        sbdotnet.Logger.Warning($"PixelFormat {buffer.Format} is not supported");
                    }
                }
            }
            OnSymbolColorChanged();
        }
    }
}
