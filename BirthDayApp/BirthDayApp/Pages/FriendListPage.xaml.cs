using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendListPage : ContentPage
    {
        private Action standartPushAddItemPage;
        private Action customPushAddItemPage;
        private Action selectPushAddItemPage;
        private ObservableCollection<Friend> items;
        public FriendListPage()
        {
            InitializeComponent();
            selectPushAddItemPage = standartPushAddItemPage = () =>
            {
                AddItemPage page = new AddItemPage();
                page.DoneEvent += Page_Close;
                page.DoneEvent += AddFriend;
                page.CancelEvent += Page_Close;
                Navigation.PushModalAsync(page);
            };
            customPushAddItemPage = () =>
            {
                WebFriendsListPage page = new WebFriendsListPage();
                page.AddFriendEvent += AddFriend;
                page.AddFriendEvent += Page_Close;
                Navigation.PushModalAsync(page);
            };
        }
        public void SetItemSource(ObservableCollection<Friend> friends)
        {
            listView.ItemsSource = (items = friends);
        }
        public void PushAddItemPage(object sender, EventArgs e)
        {
            selectPushAddItemPage();
        }

        private void Page_Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Friend item = e.SelectedItem as Friend;
            if (item != null)
            {
                PushEditItemPage(item);
            }
            ((ListView)sender).SelectedItem = null;
        }
        private void PushEditItemPage(Friend f)
        {
            EditItemPage page = new EditItemPage(f);
            page.DoneEvent += Page_Close;
            page.DoneEvent += (sender, e) => items[items.IndexOf(f)] = f;
            page.CancelEvent += Page_Close;
            page.RemoveEvent += RemoveFriend;
            page.RemoveEvent += Page_Close;
            Navigation.PushModalAsync(page);
        }
        private void RemoveFriend(object sender, EventArgs e)
        {
            EditItemPage page = sender as EditItemPage;
            items.Remove(page?.Friend);
        }
        private void AddFriend(object sender, EventArgs e)
        {
            AddItemPage page = sender as AddItemPage;
            items.Add(page?.Friend);
        }
        public void IntegrationVK()
        {
            selectPushAddItemPage = customPushAddItemPage;
        }
        public void DisintegrationVK()
        {
            selectPushAddItemPage = standartPushAddItemPage;
        }

    }
}