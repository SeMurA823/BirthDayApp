using BirthDayApp.Items;
using BirthDayApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using BirthDayApp.VkManager;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace BirthDayApp
{
    public partial class App : Application
    {
        private const string PATH_FRIEND_LIST = "friendlist.fu";
        private const string PATH_CONFIGURATION = "configuration.fr";
        private const string APP_ID = "7629034";
        private int countBeforeDays = 30;
        public static VkManager.VkManager Manager = new VkManager.VkManager();
        private ObservableCollection<Friend> friends;
        private ObservableCollection<Friend> friendsTodayBirth;
        private ObservableCollection<Friend> friendsNearBirth;
        private Configuration configuration;
        private readonly TabbedPageMain mainPage;

        public event EventHandler<AuthEventArgs> PrintAuth;
        public App()
        {
            InitializeComponent();
            MainPage = (mainPage = new TabbedPageMain());   
        }

        protected override void OnStart()
        {
            ReadConfiguration();
            Manager.access_token = configuration.Token;
            ReadFriends();
            //mainPage.FriendListPage.WriteFriend += WriteFriends;
            mainPage.FriendListPage.SetItemSource(friends);
            mainPage.MainPage.NearestMainPage.SetItemSource(friendsNearBirth);
            mainPage.MainPage.TodayMainPage.SetItemSource(friendsTodayBirth);
            friends.CollectionChanged += WriteFriends;
            mainPage.SettingsPage.LogIn += Login;
            mainPage.SettingsPage.LogOut += Logout;
            if (configuration?.Token != null)
            {
                IntegrationVk();
            } 
            
            
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
        private void WriteFriends(object sender, EventArgs e)
        {
            WriteFriends();
        }
        private void ReadFriends()
        {
            List<Friend> list;
            
            if ((list = ReadItems<List<Friend>>(PATH_FRIEND_LIST)) == null) friends = new ObservableCollection<Friend>();
            else friends = new ObservableCollection<Friend>(list);

            friendsTodayBirth = new ChildCollection<Friend>(friends, x=>x.TodayBirthDay());
            friendsNearBirth = new ChildCollection<Friend>(friends, x => (x.BeforeBirthDay() > 0 && x.BeforeBirthDay() < countBeforeDays));
        }
        private void ReadConfiguration()
        {
            if ((configuration = ReadItems<Configuration>(PATH_CONFIGURATION)) == null) 
                configuration = new Configuration();
            Manager.access_token = configuration.Token;
        }
        private void WriteFriends()
        {
            WriteItems<ObservableCollection<Friend>>(friends, PATH_FRIEND_LIST);
        }
        private void WriteConfiguration()
        {
            WriteItems<Configuration>(configuration, PATH_CONFIGURATION);
        }
        private void WriteItems<T>(T t, string path)
        {
            string localPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, path);
            string json = JsonConvert.SerializeObject(t);
            File.WriteAllText(localPath, json);
        }
        private T ReadItems<T>(string path)
        {
            string localPath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, path);
            string json;
            try
            {
                json = File.ReadAllText(localPath);
            }
            catch (IOException e)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        private void Login(object sender, EventArgs e)
        {
            var auth = new OAuth2Authenticator(
                clientId: APP_ID,
                scope: "friends",
                authorizeUrl: new Uri("https://oauth.vk.com/authorize"),
                redirectUrl: new Uri("https://oauth.vk.com/blank.html")
                );
            auth.AllowCancel = true;
            auth.Completed += (obj, ee) =>
            {
                if (ee.IsAuthenticated)
                {
                    configuration.Token = ee.Account.Properties["access_token"].ToString();
                    WriteConfiguration();
                    IntegrationVk();
                }
            };
            auth.ShowErrors = false;
            PrintAuth.Invoke(this, new AuthEventArgs(auth));
        }
        private void Logout(object sender, EventArgs e)
        {
            configuration.Token = String.Empty;
            WriteConfiguration();
            DisintegrationVK();
        }
        public class AuthEventArgs : EventArgs
        {
            public OAuth2Authenticator Auth { get; private set; }
            public AuthEventArgs(OAuth2Authenticator auth)
            {
                Auth = auth;
            }
        }
        private void IntegrationVk()
        {
            mainPage.SettingsPage.IntegrationVK();
            mainPage.FriendListPage.IntegrationVK();
        }
        private void DisintegrationVK()
        {
            mainPage.SettingsPage.DisintegraionVK();
        }
    }
}
