using System.ComponentModel;

namespace LostArkAuctionCalculrator.VM
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    public void OnPropertyChanged(string Name) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Name));
  }
}