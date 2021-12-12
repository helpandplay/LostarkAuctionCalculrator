using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LostArkAuctionCalculrator.CMD;

namespace LostArkAuctionCalculrator.VM
{
  public class CalculratorViewModel : ViewModelBase
  {
    private readonly Calculrator view;
    private readonly MainWindow parent;
    private readonly string InitAuctionString;
    private readonly string FailInfoString;
    private bool _People4;
    private bool _People8;
    private string _Auction;
    private string _Info;


    public string Auction
    {
      get => this._Auction;
      set
      {
        this._Auction = value;
        this.OnPropertyChanged("Auction");
      }
    }
    public string Info
    {
      get => this._Info;
      set
      {
        this._Info = value;
        this.OnPropertyChanged("Info");
      }
    }
    public bool People4
    {
      get => this._People4;
      set
      {
        this._People4 = value;
        this.OnPropertyChanged("People4");
        this.OnAuctionTextInput(this.Auction);
      }
    }
    public bool People8
    {
      get => this._People8;
      set
      {
        this._People8 = value;
        this.OnPropertyChanged("People8");
        this.OnAuctionTextInput(this.Auction);
      }
    }

    private ICommand _LostFocusCmd;
    public ICommand LostFocusCmd
    {
      get
      {
        if (this._LostFocusCmd == null) this._LostFocusCmd = new DelegateCommand<object>(this.OnLostFocus);
        return this._LostFocusCmd;
      }
    }
    private ICommand _GotFocusCmd;
    public ICommand GotFocusCmd
    {
      get
      {
        if (this._GotFocusCmd == null) this._GotFocusCmd = new DelegateCommand<object>(this.OnGotFocus);
        return this._GotFocusCmd;
      }
    }
    private ICommand _AuctionTextInputCmd;
    public ICommand AuctionTextInputCmd
    {
      get
      {
        if (this._AuctionTextInputCmd == null) this._AuctionTextInputCmd = new DelegateCommand<string>(this.OnAuctionTextInput);
        return this._AuctionTextInputCmd;
      }
    }
    private void OnAuctionTextInput(string quote)
    {
      if (quote == this.InitAuctionString || string.IsNullOrEmpty(quote)) return;
      if (!this.People4 && !this.People8) return;

      bool isSucess = int.TryParse(quote, out int value);

      if (!isSucess)
      {
        this.Info = this.FailInfoString;
        return;
      }
      double result = 0;
      double distribution = 0;
      if (this.People4)
      {
        result = value * 0.92 * 3 / 4;
        distribution = value / 4;

      }
      if (this.People8)
      {
        result = value * 0.92 * 7 / 8;
        distribution = value / 7;
      }

      this.Info = $"입찰 : {(int)result} | 분배 : {(int)distribution / 4}";
    }
    private void OnGotFocus(object param)
    {

    }
    private void OnLostFocus(object param)
    {
      this.view.Visibility = Visibility.Hidden;
      this.view.Topmost = false;
      this.view.Close();
    }
    public void OnMouseFocus(object sender, RoutedEventArgs e)
    {
      if (!(sender is TextBox)) return;
      var textBox = sender as TextBox;

      textBox.Focus();
      textBox.Text = "";
    }
    private void SetInitPosition()
    {
      if (this.parent == null) return;
      if (this.view == null) return;

      this.view.Topmost = true;

      double parentLeft = this.parent.Left;
      double parentTop = this.parent.Top;
      double width = this.view.Width;
      double height = this.view.Height;

      this.view.Left = parentLeft - width;
      this.view.Top = parentTop - height + this.parent.ActualHeight;
    }
    public CalculratorViewModel(Calculrator view, MainWindow parent)
    {
      this.view = view;
      this.parent = parent;
      this.SetInitPosition();

      this.InitAuctionString = "Input here!";
      this.FailInfoString = "계산할 수 없는 입력값입니다.";
      this.Auction = this.InitAuctionString;
      this.Info = "시세를 입력하세요";
      this.People8 = true;
    }
  }
}
