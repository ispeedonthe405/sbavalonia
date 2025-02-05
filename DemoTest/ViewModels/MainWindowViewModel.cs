using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using sbavalonia.symbols;
using System.Diagnostics;

namespace DemoTest.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        public string symName = "add";

        [RelayCommand]
        public void ToggleSymbol()
        {
            SymbolManager.SymbolColor = Colors.Aquamarine;
        }


        public MainWindowViewModel()
        {
            
        }
    }
}
