using Avalonia.Input;

namespace sbavalonia.controls
{
    public class NumberBox
    {
        private static bool KeyIsNumberOrControl(Key key)
        {
            if (
               key.Equals(Key.D0) || key.Equals(Key.NumPad0) ||
               key.Equals(Key.D1) || key.Equals(Key.NumPad1) ||
               key.Equals(Key.D2) || key.Equals(Key.NumPad2) ||
               key.Equals(Key.D3) || key.Equals(Key.NumPad3) ||
               key.Equals(Key.D4) || key.Equals(Key.NumPad4) ||
               key.Equals(Key.D5) || key.Equals(Key.NumPad5) ||
               key.Equals(Key.D6) || key.Equals(Key.NumPad6) ||
               key.Equals(Key.D7) || key.Equals(Key.NumPad7) ||
               key.Equals(Key.D8) || key.Equals(Key.NumPad8) ||
               key.Equals(Key.D9) || key.Equals(Key.NumPad9) ||
               key.Equals(Key.Tab) || key.Equals(Key.Back))
            {
                return true;
            }
            return false;
        }

        public static void OnKey(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            if (!KeyIsNumberOrControl(e.Key))
            {
                e.Handled = true;
            }
        }
    }
}
