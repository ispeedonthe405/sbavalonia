using Avalonia.Controls;
using Avalonia.Media;
using sbavalonia.symbols;

namespace DemoTest.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SymbolManager.SymbolColor = Colors.Orange;
            sym.ApplySourceToSymbol();
            sym.SymbolName = "add";
            sym.ApplySourceToSymbol();
        }
    }
}