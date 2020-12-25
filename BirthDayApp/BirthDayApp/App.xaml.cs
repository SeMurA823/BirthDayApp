using BirthDayApp.Items;
using BirthDayApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using BirthDayApp.SocialManager;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BirthDayApp.themes;
using static BirthDayApp.Pages.SettingPages.EditColorSchemePage;
using SocialManager.VkManager;

namespace BirthDayApp
{
    public partial class App : Application
    {
        /* Пути файлов */
        private const string PATH_FRIEND_LIST = "friendlist.fu";
        private const string PATH_CONFIGURATION = "configuration.fr";
        /* Номер приложения */
        private const string APP_ID = "7629034";
        public VkManager Manager { get; private set; } // VK
        public ThemeManager ThemeManager { get; private set; } //Темы
        public UI GUI { get; set; } // GUI

        private int countBeforeDays = 30; // За сколько дней до, помещать юзера в "скоро"
        
        private ObservableCollection<Friend> friends; // общий список друзей
        private ObservableCollection<Friend> friendsTodayBirth; // сегодня день рождения
        private ObservableCollection<Friend> friendsNearBirth; // скоро день рождения

        private Configuration configuration; // конфиг

        private readonly TabbedPageMain mainPage;
        public App(UI gui)
        {
            InitializeComponent();

            Manager = VkManager.GetManager();
            ReadConfiguration();
            ThemeManager = new ThemeManager(customTheme, configuration.Theme, gui);
            GUI = gui;
            Device.SetFlags(new string[] { "AppTheme_Experimental" });
            MainPage = (mainPage = new TabbedPageMain());
        }

        protected override void OnStart()
        {
            Manager.AccessToken = configuration.Token;
            ReadFriends();
            //mainPage.FriendListPage.WriteFriend += WriteFriends;
            mainPage.FriendListPage.SetItemSource(friends);
            mainPage.MainPage.NearestMainPage.SetItemSource(friendsNearBirth);
            mainPage.MainPage.TodayMainPage.SetItemSource(friendsTodayBirth);
            friends.CollectionChanged += WriteFriends;
            if (configuration?.Token != null)
            {
                IntegrationVk();
            } 
            mainPage.SettingsPage.LoginEvent += Login;
            mainPage.SettingsPage.LogoutEvent += Logout;
            mainPage.SettingsPage.EditColorSchemeEvent += EditTheme;
            
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
        private void EditTheme(object sender, ThemeEventArgs e)
        {
            ThemeManager.SetTheme(configuration.Theme = e.Theme); // установка темы
            WriteConfiguration();
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
            if ((configuration = ReadItems<Configuration>(PATH_CONFIGURATION)) == null) configuration = new Configuration();
            Manager.AccessToken = configuration.Token;
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
            catch (IOException)
            {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(json);
        }
        private void Login(object sender, EventArgs e)
        {
            Manager.Auth(GUI, APP_ID, IntegrationVk);
        }
        private void Logout(object sender, EventArgs e)
        {
            DisintegrationVK();
        }
        private void IntegrationVk() // интеграция ВК
        {
            configuration.Token = Manager.AccessToken;
            WriteConfiguration();
            mainPage.SettingsPage.IntegrationVK();
            mainPage.FriendListPage.IntegrationVK();
        }
        private void DisintegrationVK() // дисинтеграция ВК
        {
            configuration.Token = null;
            WriteConfiguration();
            mainPage.FriendListPage.DisintegrationVK();
            mainPage.SettingsPage.DisintegrationVK();
        }
    }

    public interface UI
        :UIAuth, UIThemeEditable
    {

    }
}
