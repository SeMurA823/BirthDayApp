using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.MainPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodayMainPage : ContentPage
    {
        public DateCounter Counter { get; private set; }

        private ObservableCollection<Friend> friendList;
        public TodayMainPage()
        {
            InitializeComponent();
            if (DateTime.Today.Month == 12)
            {
                counterNewYear.IsVisible = true;
                Counter = new DateCounter(new DateTime(DateTime.Today.Year + 1, 1, 1));
                Counter.TimeChangeEvent += TimeChanged;
            }
            else counterNewYear.IsVisible = false;
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
        private void TimeChanged(object sender, EventArgs eventArgs)
        {
            counter.Text = string.Format(
                "{0}дн {1}ч {2}м",
                Counter.Time.Days, 
                Counter.Time.Hours,
                Counter.Time.Minutes);
        }
    }
}