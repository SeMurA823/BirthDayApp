using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BirthDayApp.Pages;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace BirthDayApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPageMain : Xamarin.Forms.TabbedPage
    {
        public SettingsPage SettingsPage;
        public MainPage MainPage;
        public FriendListPage FriendListPage;
        public TabbedPageMain()
        {
            /* Перемещение тулбара вниз на android*/
            On<Android>().SetToolbarPlacement(value: ToolbarPlacement.Bottom);

            SettingsPage = new SettingsPage();
            MainPage = new MainPage();
            FriendListPage = new FriendListPage();
            
            Children.Add(new SettingsNavigationPage(SettingsPage));
            Children.Add(new NavigationPage(MainPage)
            {
                Title = "Главная",
                IconImageSource = "@drawable/MainIcon.png",
                BarTextColor = Color.White
            });
            Children.Add(new FriendsListNavigationPage(FriendListPage));

            InitializeComponent();
     
        }
    }
}