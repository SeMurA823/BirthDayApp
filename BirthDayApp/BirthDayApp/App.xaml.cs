using BirthDayApp.Items;
using BirthDayApp.Pages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp
{
    public partial class App : Application
    {
        private const string PATH_FRIEND_LIST = "friendlist.fu";
        private const string PATH_CONFIGURATION = "configuration.fr";
        private ObservableCollection<Friend> friends;
        private ObservableCollection<Friend> friendsFavorites;
        private Configuration configuration;
        private TabbedPageMain mainPage;
        public App()
        {
            InitializeComponent();
            friends = ReadFriends();
            friendsFavorites = ReadFavoriteFriends();
            mainPage = new TabbedPageMain();
            MainPage = mainPage;
            mainPage.FriendListPage.WriteFriend += WriteFriends;
            mainPage.FriendListPage.SetItemSource(friends);
            mainPage.MainPage.SetItemSource(friendsFavorites);
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

        private ObservableCollection<Friend> ReadFriends()
        {
            Friend[] friends;
            if ((friends = ReadItems<Friend[]>(PATH_FRIEND_LIST)) == null) return new ObservableCollection<Friend>();
            return new ObservableCollection<Friend>(friends);
        }
        private ObservableCollection<Friend> ReadFavoriteFriends()
        {
            ObservableCollection<Friend> list = new ObservableCollection<Friend>();
            foreach (var el in list)
                if (el.IsFavorite) list.Add(el);
            return list;
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
