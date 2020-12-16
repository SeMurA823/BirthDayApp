using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BirthDayApp.Pages;
using BirthDayApp.Items;
using System.Collections.ObjectModel;

namespace BirthDayApp.Pages
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Friend> FriendsFavorites;
        public MainPage()
        {
            InitializeComponent();
        }
        public void SetItemSource(ObservableCollection<Friend> friends)
        {
            listView.ItemsSource = (FriendsFavorites = new ObservableCollection<Friend>(friends));
        }
        
    }
}
