using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BirthDayApp.Pages
{
    public class FriendsListNavigationPage : NavigationPage
    {
        public FriendsListNavigationPage (FriendListPage root) : base(root)
        {
            Title = "Список";
            IconImageSource = "@drawable/FriendsIcon.png";
            BarTextColor = Color.White;
            ToolbarItem item = new ToolbarItem
            {
                IconImageSource = "@drawable/AddIcon.png",
            };
            item.Clicked += root.PushAddItemPage;
            ToolbarItems.Add(item);
        }        
    }
}
