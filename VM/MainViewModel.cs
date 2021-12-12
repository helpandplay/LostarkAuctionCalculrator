using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using LostArkAuctionCalculrator.CMD;

namespace LostArkAuctionCalculrator.VM
{
  public enum MenuOption
  {
    Exit
  };
  public class MainViewModel : ViewModelBase
  {

    private readonly MainWindow view;
    private NotifyIcon tray;
    private Window calculrator;


    private ICommand _LoadedCmd;
    private ICommand _DeActivatedCmd;
    public ICommand LoadedCmd
    {
      get
      {
        if (this._LoadedCmd == null) this._LoadedCmd = new DelegateCommand<object>(this.OnLoaded);
        return this._LoadedCmd;
      }
    }
    public ICommand DeActivatedCmd
    {
      get
      {
        if (this._DeActivatedCmd == null) this._DeActivatedCmd = new DelegateCommand<object>(this.OnDeActivated);
        return this._DeActivatedCmd;
      }
    }


    public void OnMouseDrag(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton != MouseButtonState.Pressed) return;
      if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))) return;
      Point point = e.GetPosition(this.view);
      this.view.DragMove();
    }
    public void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
      if (e.ClickCount != 2) return;
      if (this.calculrator != null)
      {
        this.calculrator.Close();
      }
      this.calculrator = new Calculrator(this.view);
      this.calculrator.Show();
    }
    private void OnLoaded(object param)
    {
      this.SetInitPosition();
      this.SetTrayMenu();

    }
    private void OnDeActivated(object param)
    {
      if (this.view.Topmost) return;
      this.view.Topmost = true;
    }
    private void CloseMenu_Click(object sender, EventArgs e)
    {
      var menuItem = sender as MenuItem;
      if (menuItem == null) return;

      switch ((MenuOption)Enum.Parse(typeof(MenuOption), menuItem.Tag.ToString()))
      {
        case MenuOption.Exit:
          this.DeleteTrayIcon();
          if (this.calculrator != null) this.calculrator.Close();
          this.view.Close();
          break;
        default:
          break;
      }
    }
    private void SetInitPosition()
    {
      double x = SystemParameters.PrimaryScreenWidth;
      double y = SystemParameters.PrimaryScreenHeight;

      double width = this.view.Width;
      double height = this.view.Height;

      this.view.Left = (x / 2) - (width / 2);
      this.view.Top = (y / 2) - (height / 2);
    }
    private void SetTrayMenu()
    {
      this.tray = new NotifyIcon();
      var contextMenu = new ContextMenu();
      var closeMenu = new MenuItem()
      {
        Index = 0,
        Text = "Exit",
        Tag = MenuOption.Exit.ToString()
      };
      closeMenu.Click += this.CloseMenu_Click;

      contextMenu.MenuItems.Add(closeMenu);

      this.tray.Visible = true;
      this.tray.Icon = Properties.Resources.icon;
      this.tray.ContextMenu = contextMenu;
      this.tray.Text = "경매금 계산기";
    }
    private void DeleteTrayIcon()
    {
      if (this.tray == null) return;
      this.tray.Visible = false;
      this.tray.ContextMenu = null;
    }

    public MainViewModel(MainWindow view) => this.view = view;
  }
}
