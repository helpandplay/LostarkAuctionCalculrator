using System.Threading;
using System.Windows;

namespace LostArkAuctionCalculrator
{
  /// <summary>
  /// App.xaml에 대한 상호 작용 논리
  /// </summary>
  public partial class App : Application
  {
    Mutex mutex;
    protected override void OnStartup(StartupEventArgs e)
    {
      base.OnStartup(e);

      string mutexName = "program";
      bool createNew;

      mutex = new Mutex(true, mutexName, out createNew);

      if (!createNew)
      {
        MessageBox.Show("이미 실행되고 있는 프로그램입니다. 트레이를 확인해주세요.");
        this.Shutdown();
      }
    }

  }
}
