using System.Windows;
using LostArkAuctionCalculrator.VM;

namespace LostArkAuctionCalculrator
{
  /// <summary>
  /// MainWindow.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      this.InitializeComponent();
      this.DataContext = new MainViewModel(this);
    }
  }
}
