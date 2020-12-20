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
            listView.ItemsSource = WebFriend.GetListFriend();
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
        public class WebFriend : Friend{
            public Image Photo { get; set; }
            public static List<WebFriend> GetListFriend()
            {
                List<Friend> list = App.Manager.GetFriends();
                List<WebFriend> webFriends = new List<WebFriend>(list.Count);
                list.ForEach(x => webFriends.Add(Convert(x)));
                return webFriends;
            }
            private static WebFriend Convert(Friend fr)
            {
                return new WebFriend
                {
                    FirstName = fr.FirstName,
                    LastName = fr.LastName,
                    BDate = fr.BDate,
                    Photo = new Image
                    {
                        Source = new UriImageSource
                        {
                            Uri = new Uri(fr.Photo200),
                            CachingEnabled = false
                        },
                        WidthRequest = 72,
                        HeightRequest = 72,
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center
                    }
                };
            }
        }

    }
}