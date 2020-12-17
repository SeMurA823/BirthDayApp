using BirthDayApp.Items;
using BirthDayApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp
{
    public partial class App : Application
    {
        private const string PATH_FRIEND_LIST = "friendlist.fu";
        private const string PATH_CONFIGURATION = "configuration.fr";
        private int countBeforeDays = 30;
        private ObservableCollection<Friend> friends;
        private ObservableCollection<Friend> friendsTodayBirth;
        private ObservableCollection<Friend> friendsNearBirth;
        private Configuration configuration;
        private TabbedPageMain mainPage;
        public App()
        {
            InitializeComponent();
            ReadFriends();
            mainPage = new TabbedPageMain();
            MainPage = mainPage;
            mainPage.FriendListPage.WriteFriend += WriteFriends;
            mainPage.FriendListPage.SetItemSource(friends);
            mainPage.MainPage.NearestMainPage.SetItemSource(friendsNearBirth);
            mainPage.MainPage.TodayMainPage.SetItemSource(friendsTodayBirth);
            friends.CollectionChanged += WriteFriends;
        }

        protected override void OnStart()
        {
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
            Friend[] arr;
            
            if ((arr = ReadItems<Friend[]>(PATH_FRIEND_LIST)) == null) friends = new ObservableCollection<Friend>();
            else friends = new ObservableCollection<Friend>(arr);

            friendsTodayBirth = new ChildCollection<Friend>(friends, x=>x.TodayBirthDay());
            friendsNearBirth = new ChildCollection<Friend>(friends, x => (x.BeforeBirthDay() > 0 && x.BeforeBirthDay() < countBeforeDays));
        }
        private void WriteFriends()
        {
            WriteItems<ObservableCollection<Friend>>(friends, PATH_FRIEND_LIST);
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
    }
}
