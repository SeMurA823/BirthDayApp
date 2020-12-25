using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodayMainPage : ContentPage
    {
        private ObservableCollection<Friend> friendList;
        public TodayMainPage()
        {
            InitializeComponent();
        }
        public void SetItemSource(ObservableCollection<Friend> friends)
        {
            todayList.ItemsSource = (friendList = friends);
            FriendListChanged();
            friendList.CollectionChanged += FriendListChanged;
        }

        private void FriendListChanged()
        {
            if (friendList == null || friendList.Count == 0)
            {
                imageHeader.Source = "@drawable/sad.png";
            }
            else
            {
                imageHeader.Source = "@drawable/balloons.png";
            }
        }
        private void FriendListChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            FriendListChanged();
        }
    }
}