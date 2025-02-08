using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace sbavalonia.media
{
    public static class ImageUtil
    {
        public static unsafe void RecolorMonochromeBitmap(ref WriteableBitmap bitmap, Color newColor)
        {
            try
            {
                using ILockedFramebuffer buffer = bitmap.Lock();
                var bytesPerPixel = 4;
                var pixelptr = (byte*)buffer.Address;
                int pixelCountMax = bitmap.PixelSize.Width * bitmap.PixelSize.Height;

                for (int pixelCurrent = 0; pixelCurrent < pixelCountMax; pixelCurrent++)
                {
                    var pixel = new Span<byte>(pixelptr + (pixelCurrent * bytesPerPixel), bytesPerPixel);

                    PixelFormat format = buffer.Format;
                    if (format == PixelFormat.Rgba8888)
                    {
                        // skip the transparent pixels
                        if (pixel[3] == 0) continue;

                        pixel[0] = newColor.R;
                        pixel[1] = newColor.G;
                        pixel[2] = newColor.B;
                        pixel[3] = newColor.A;
                    }
                    else if (format == PixelFormat.Bgra8888)
                    {
                        // skip the transparent pixels
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
    }
}
