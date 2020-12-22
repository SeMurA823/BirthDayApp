using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.NavigationPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FriendsListNavigationPage : NavigationPage
    {
        public FriendsListNavigationPage(FriendListPage root) : base(root)
        {
            InitializeComponent();
            ToolbarItem item = new ToolbarItem
            {
                IconImageSource = "@drawable/AddIcon.png",
            };
            item.Clicked += root.PushAddItemPage;
            ToolbarItems.Add(item);
        }
    }
}