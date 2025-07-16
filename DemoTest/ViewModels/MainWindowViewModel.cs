using Avalonia;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using sbavalonia.symbols;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace DemoTest.ViewModels
{
    public class TestData
    {
        long val = DateTime.Now.Ticks;
        public long Value
        {
            get => val;
            set => val = value;
        }
    }

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

        private bool _ColorToggle = false;
        [RelayCommand]
        void ToggleSymbol()
        {
            _ColorToggle = !_ColorToggle;
            if (_ColorToggle)
            {
                SymbolManager.LightThemeColor = Colors.Purple;
                SymbolManager.DarkThemeColor = Colors.LightGreen;
            }
            else
            {
                SymbolManager.ResetColors();
            }
        }

        [RelayCommand]
        void ToggleTheme()
        {
            if (SelectedTheme.Equals(ThemeVariant.Default))
            {
                SelectedTheme = ThemeVariant.Light;
            }
            else if(SelectedTheme.Equals(ThemeVariant.Light))
            {
                SelectedTheme = ThemeVariant.Dark;
            }
            else
            {
                SelectedTheme = ThemeVariant.Light;
            }
        }

        [ObservableProperty]
        string _BoundText = "I am bound text. FEAR ME!";

        [ObservableProperty]
        int _BoundInt = 12345;

        [ObservableProperty]
        ObservableCollection<TestData> _GridData = [];

        [ObservableProperty]
        TestData? _SelectedData;

        [RelayCommand]
        void TestGrid()
        {
            
        }


        public MainWindowViewModel()
        {
            for (int i = 0; i < 50; i++)
            {
                GridData.Add(new());
            }
            SelectedData = GridData.First();
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
