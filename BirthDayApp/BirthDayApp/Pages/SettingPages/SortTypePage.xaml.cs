using BirthDayApp.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BirthDayApp.Pages.SettingPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SortTypePage : ContentPage
    {
        private TypeSortFriend typeSortFriend;
        private Button selectBtn;
        public SortTypePage(TypeSortFriend typeSort)
        {
            typeSortFriend = typeSort;
            InitializeComponent();
            StartBtn();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackgroundColor = Color.DarkSlateBlue;
            selectBtn = btn;
            typeSortFriend = GetTypeSort(btn);
            UpdateBtn();
        }
        private void UpdateBtn()
        {
            foreach(var btn in stackButtons.Children)
            {
                if (btn != selectBtn) btn.BackgroundColor = Color.SlateBlue;
            }
        }
        private TypeSortFriend GetTypeSort(Button btn)
        {
            if (btn == abc_a_z) return TypeSortFriend.ABC_A_Z;
            if (btn == abc_z_a) return TypeSortFriend.ABC_Z_A;
            return TypeSortFriend.NORMAL;
        }
        private void StartBtn()
        {
            if (typeSortFriend == TypeSortFriend.NORMAL)
            {
                defaultSort.BackgroundColor = Color.DarkSlateBlue;
                selectBtn = defaultSort;
                return;
            }
            if (typeSortFriend == TypeSortFriend.ABC_A_Z)
            {
                abc_a_z.BackgroundColor = Color.DarkSlateBlue;
                selectBtn = abc_a_z;
                return;
            }
            if (typeSortFriend == TypeSortFriend.ABC_Z_A)
            {
                abc_z_a.BackgroundColor = Color.DarkSlateBlue;
                selectBtn = abc_z_a;
                return;
            }
        }

        private class ButtonTypeSort
        {
            public Button btn;
            public TypeSortFriend typeSort;
        }
    }
}