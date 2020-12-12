using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using BirthDayApp.Pages;
using BirthDayApp.Items;

namespace BirthDayApp
{
    public partial class MainPage : ContentPage
    {
        private List<Friend> Friends;
        public event EventHandler WriteFriendEvent;
        public MainPage(List<Friend> list)
        {
            InitializeComponent();
            Friends = list;
            PrintFriends();
        }

        private void add_Clicked(object sender, EventArgs e)
        {
            AddItemPage page = new AddItemPage();
            page.DoneEvent += WriteFriendEvent.Invoke;
            page.DoneEvent += Page_Close;
            page.CancelEvent += Page_Close;
            Navigation.PushModalAsync(page);
        }

        private void settings_Clicked(object sender, EventArgs e)
        {

        }

        private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {

        }
        private void Page_Close(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        public void PrintFriends()
        {
            if (Friends.Count == 0)
            {
                listIsEmpty.IsVisible = true;
                return;
            }
            listIsEmpty.IsVisible = false;
            ContainerFriends.Children.Clear();
            int row = 0;
            foreach (var f in Friends)
            {
                ContainerFriends.Children.Add(CreateFrameImage(f), 0, row);
                ContainerFriends.Children.Add(CreateStackLayout(f), 1, row);
                row++;
            }
        }
        private Frame CreateFrameImage(Friend f)
        {
            Image image = new Image
            {
                Source = f.PathImage,
                WidthRequest = 100,
                HeightRequest = 100
            };
            return new Frame
            {
                Content = image,
                Style = FrameImageStyle
            };
        }
        private StackLayout CreateStackLayout(Friend f)
        {
            string startBeforeBdate = "До дня рождения осталось ";
            StackLayout stack = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.Center
            };
            Label FirstNameLabel = new Label
            {
                Text = f.FirstName,
                Style = LabelNameStyle
            };
            stack.Children.Add(FirstNameLabel);
            Label LastNameLabel = new Label
            {
                Text = f.LastName,
                Style = LabelNameStyle
            };
            stack.Children.Add(LastNameLabel);
            Label BDate = new Label
            {
                Text = f.BirthDate.ToShortDateString(),
                Style = LabelBdateStyle
            };
            stack.Children.Add(BDate);
            int days;
            Label BeforeDay = new Label
            {
                Text = ((days = (DateTime.Today - f.GetBirthDay()).Days) < 30) ? startBeforeBdate + StringDays(days) : " ",
                Style = BeforeDateStyle
            };
            stack.Children.Add(BeforeDay);
            return stack;
        }
        private static string StringDays(int days)
        {
            if (days % 10 == 1) return days + " день";
            if ((days % 10 >= 2) && (days % 10 <= 4)) return days + " дня";
            return days + " дней";
        }
    }
}
