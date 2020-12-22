using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthDayApp.VkManager.items;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebFriendsListPage : ContentPage
    {
        public event EventHandler AddFriendEvent;
        public WebFriendsListPage()
        {
            InitializeComponent();
            listView.ItemsSource = App.Manager.GetFriends();
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                AddItemPage page = new AddItemPage(e.SelectedItem as Friend);
                page.DoneEvent += Page_Close;
                page.DoneEvent += AddFriendEvent;
                page.CancelEvent += Page_Close;
                Navigation.PushModalAsync(page);
            }
            ((ListView)sender).SelectedItem = null;
            
        }
        private void Page_Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AddItemPage page = new AddItemPage();
            page.DoneEvent += Page_Close;
            page.DoneEvent += AddFriendEvent;
            page.CancelEvent += Page_Close;
            Navigation.PushModalAsync(page);
        }
    }
}