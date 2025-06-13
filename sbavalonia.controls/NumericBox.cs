using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sbavalonia.controls
{
    public class NumericBox : Avalonia.Controls.TextBox
    {
        protected override Type StyleKeyOverride { get; } = typeof(TextBox);

        private Avalonia.Controls.TextBlock Symbol = new();

        public static readonly StyledProperty<bool> IsDecimalProperty =
            AvaloniaProperty.Register<NumericBox, bool>(nameof(IsDecimal), false);
        
        public bool IsDecimal
        {
            get => GetValue(IsDecimalProperty);
            set => SetValue(IsDecimalProperty, value);
        }

        public static readonly StyledProperty<string> OverlaySymbolProperty =
            AvaloniaProperty.Register<NumericBox, string>(nameof(OverlaySymbol), "$");

        public string OverlaySymbol
        {
            get => GetValue(OverlaySymbolProperty);
            set
            {
                SetValue(OverlaySymbolProperty, value);
                Symbol.Text = value;
            }
        }

        private bool KeyIsNumberOrControl(Key key, KeyModifiers mod)
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
               key.Equals(Key.D9) || key.Equals(Key.NumPad9))
            {
                if (mod == KeyModifiers.None) return true;
            }

            else if (key.Equals(Key.Tab) || key.Equals(Key.Back) || key.Equals(Key.Delete))
            {
                return true;
            }

            else if (key.Equals(Key.OemPeriod) || key.Equals(Key.Decimal))
            {
                return true;
            }

            return false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!KeyIsNumberOrControl(e.Key, e.KeyModifiers))
            {
                e.Handled = true;
                return;
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (!KeyIsNumberOrControl(e.Key, e.KeyModifiers))
            {
                e.Handled = true;
                return;
            }
            base.OnKeyUp(e);
        }

        public NumericBox()
            : base()
        {
            //Symbol.IsHitTestVisible = false;
            Symbol.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
            Symbol.ZIndex = this.ZIndex + 1;
            Symbol.Foreground = new SolidColorBrush(Colors.YellowGreen);
            this.LogicalChildren.Add(Symbol);
            this.VisualChildren.Add(Symbol);
            //this.Padding = new Thickness(15, 5, 0, 0);

            Loaded += NumericBox_Loaded;
        }

        private void NumericBox_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //Symbol.Arrange(Bounds);
        }
    }
}
