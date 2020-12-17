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
            if (friendList == null || friendList.Count == 0)
            {
                empty.IsVisible = true;
                todayList.IsVisible = false;
            }
            else
            {
                empty.IsVisible = false;
                todayList.IsVisible = true;
            }
            friendList.CollectionChanged += FriendListChanged;
        }

        private void FriendListChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (friendList == null || friendList.Count == 0)
            {
                empty.IsVisible = true;
                todayList.IsVisible = false;
            }
            else
            {
                empty.IsVisible = false;
                todayList.IsVisible = true;
            }
        }
    }
}