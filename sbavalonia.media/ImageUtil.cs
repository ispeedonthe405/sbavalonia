using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace sbavalonia.media
{
    public static class ImageUtil
    {
#if true
        public static unsafe void RecolorMonochromeBitmap(ref WriteableBitmap bitmap, Color newColor)
        {
            try
            {
                using ILockedFramebuffer buffer = bitmap.Lock();
                var bytesPerPixel = 4;
                var pixelptr = (byte*)buffer.Address;
                int pixelCountMax = bitmap.PixelSize.Width * bitmap.PixelSize.Height;
                PixelFormat format = buffer.Format;

                if (format != PixelFormat.Rgba8888 && format != PixelFormat.Bgra8888)
                {
                    sbdotnet.Logger.Warning($"PixelFormat {buffer.Format} is not supported");
                    return;
                }

                for (int pixelCurrent = 0; pixelCurrent < pixelCountMax; pixelCurrent++)
                {
                    var pixel = new Span<byte>(pixelptr + (pixelCurrent * bytesPerPixel), bytesPerPixel);

                    // skip the transparent pixels
                    if (pixel[3] == 0) continue;

                    
                    if (format == PixelFormat.Rgba8888)
                    {
                        pixel[0] = newColor.R;
                        pixel[1] = newColor.G;
                        pixel[2] = newColor.B;
                        pixel[3] = newColor.A;
                    }
                    else if (format == PixelFormat.Bgra8888)
                    {
                        pixel[0] = newColor.B;
                        pixel[1] = newColor.G;
                        pixel[2] = newColor.R;
                        pixel[3] = newColor.A;
                    }
                }
            }
            catch (Exception ex)
            {
                sbdotnet.Logger.Error(ex);
            }
        }
#endif

#if false
        public static unsafe void RecolorMonochromeBitmap(ref WriteableBitmap bitmap, Color newColor)
        {
            try
            {
                using ILockedFramebuffer buffer = bitmap.Lock();
                var bytesPerPixel = 4;
                var pixelptr = (byte*)buffer.Address;
                int width = bitmap.PixelSize.Width;
                int height = bitmap.PixelSize.Height;
                int pixelCountMax = width * height;
                PixelFormat format = buffer.Format;

                if (format != PixelFormat.Rgba8888 && format != PixelFormat.Bgra8888)
                {
                    sbdotnet.Logger.Warning($"PixelFormat {buffer.Format} is not supported");
                    return;
                }

                Parallel.For(0, height, y =>
                {
                    for (int x = 0; x < width; x++)
                    {
                        int pixelCurrent = y * width + x;
                        var pixel = new Span<byte>(pixelptr + (pixelCurrent * bytesPerPixel), bytesPerPixel);

                        // skip the transparent pixels
                        if (pixel[3] == 0) return;

                        if (format == PixelFormat.Rgba8888)
                        {
                            pixel[0] = newColor.R;
                            pixel[1] = newColor.G;
                            pixel[2] = newColor.B;
                            pixel[3] = newColor.A;
                        }
                        else if (format == PixelFormat.Bgra8888)
                        {
                            pixel[0] = newColor.B;
                            pixel[1] = newColor.G;
                            pixel[2] = newColor.R;
                            pixel[3] = newColor.A;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                sbdotnet.Logger.Error(ex);
            }
        }
#endif
    }
}
