using System.Windows;
using LostArkAuctionCalculrator.VM;

namespace LostArkAuctionCalculrator
{
  /// <summary>
  /// Calculrator.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class Calculrator : Window
  {
    public Calculrator(MainWindow main)
    {
      this.InitializeComponent();
      this.DataContext = new CalculratorViewModel(this, main);
    }
  }
}
