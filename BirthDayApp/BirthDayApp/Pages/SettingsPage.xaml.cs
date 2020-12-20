using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BirthDayApp.Pages.SettingPages;
using BirthDayApp.Items;

namespace BirthDayApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public ObservableCollection<ItemMenu> Items { get; set; }

        public event EventHandler LogIn;
        public event EventHandler LogOut;

        private ItemMenu vkSign;
        private ItemMenu vkOut;

        public SettingsPage()
        {
            InitializeComponent();
            vkSign = new ItemMenu("ВОЙТИ В ВК", () => {
                LogIn.Invoke(vkSign, EventArgs.Empty);
            });
            vkOut = new ItemMenu("ВЫЙТИ ИЗ ВК",  async () => {
                bool outBool = await DisplayAlert("Выход", "Действительно вы хотите выйти?", "Да", "Нет");
                if (outBool) LogOut.Invoke(vkOut, EventArgs.Empty);
            });
            Items = new ObservableCollection<ItemMenu>
            {
                vkSign
            };

            listView.ItemsSource = Items;
            listView.RefreshControlColor = Color.Red;
            
        }
        public class ItemMenu
        {
            public Action ActionSelectItem { get; set; }
            public string TextStr { get; set; }
            public ItemMenu(string text, Action selectItem)
            {
                TextStr = text;
                ActionSelectItem = selectItem;
            }
            
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ItemMenu item = e.SelectedItem as ItemMenu;
            item?.ActionSelectItem();
            ((ListView)sender).SelectedItem = null;
        }

        public void IntegrationVK()
        {
            Items[Items.IndexOf(vkSign)] = vkOut;
        }
        public void DisintegraionVK()
        {
            Items[Items.IndexOf(vkOut)] = vkSign;
        }
    }
}
