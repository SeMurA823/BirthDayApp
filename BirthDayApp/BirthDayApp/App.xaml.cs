using BirthDayApp.Items;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp
{
    public partial class App : Application
    {
        private const string PATH_FRIEND_LIST = "friendlist.fu";
        private const string PATH_CONFIGURATION = "configuration.fr";
        private List<Friend> friends;
        private Configuration configuration;
        private MainPage mainPage;
        public App()
        {
            InitializeComponent();
            friends = ReadFriends();
            MainPage = (mainPage = new MainPage(friends));
            mainPage.WriteFriendEvent += WriteFriend;
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
        private void WriteFriend(object sender, EventArgs e)
        {
            Friend f = sender as Friend;
            if (f != null) WriteFriend(f);
        }
        public void WriteFriend(Friend f)
        {
            friends.Add(f);
            mainPage.PrintFriends();
            WriteFriends();
        }
        private List<Friend> ReadFriends()
        {
            List<Friend> list = ReadItems<List<Friend>>(PATH_FRIEND_LIST);
            return (list == null) ? (new List<Friend>()): list;
        }
        private void WriteFriends()
        {
            WriteItems<List<Friend>>(friends, PATH_FRIEND_LIST);
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
