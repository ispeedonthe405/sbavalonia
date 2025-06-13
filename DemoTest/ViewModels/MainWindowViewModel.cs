using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using sbavalonia.symbols;
using System.Collections.Generic;
using System.Diagnostics;

namespace DemoTest.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        List<ThemeVariant> _Themes =
            [
            ThemeVariant.Default,
            ThemeVariant.Light,
            ThemeVariant.Dark
            ];

        [ObservableProperty]
        ThemeVariant _SelectedTheme = ThemeVariant.Default;

        [ObservableProperty]
        public double _NumValue = 123.45;

        [ObservableProperty]
        public string symName = "add";

        [RelayCommand]
        public void ToggleSymbol()
        {
            //SymbolManager.SymbolColor = Colors.Aquamarine;
        }


        public MainWindowViewModel()
        {
            PropertyChanged += MainWindowViewModel_PropertyChanged;
        }

        private void MainWindowViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is null) return;

            if(e.PropertyName.Equals(nameof(SelectedTheme)))
            {
                App.Current!.RequestedThemeVariant = SelectedTheme;
            }
        }
    }
}
