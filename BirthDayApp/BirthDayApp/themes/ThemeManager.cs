using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BirthDayApp.themes
{
    public enum EThemes
    {
        BLACK_YELLOW,
        INDIGO,
        BLUE
    }

    public class ThemeManager
    {
        public ResourceDictionary SelectedTheme { get; private set; }
        
        private ResourceDictionary Resource;
        private UIThemeEditable gui;
        
        private static ResourceDictionary blackYellowTheme = new BlackYellowTheme();
        private static ResourceDictionary indigoTheme = new IndigoTheme();
        private static ResourceDictionary blueTheme = new BlueTheme();
        public ThemeManager(ResourceDictionary resource, EThemes theme)
            :this(resource, theme, null)
        {
            
        }
        public ThemeManager(ResourceDictionary resource, EThemes theme, UIThemeEditable ui)
        {
            Resource = resource;
            gui = ui;
            SetTheme(theme);
        }
        public void SetTheme(EThemes theme)
        {
            switch (theme)
            {
                case EThemes.BLACK_YELLOW:
                    SelectTheme(SelectedTheme = blackYellowTheme);
                    break;
                case EThemes.BLUE:
                    SelectTheme(SelectedTheme = blueTheme);
                    break;
                case EThemes.INDIGO:
                    SelectTheme(SelectedTheme = indigoTheme);
                    break;
            }
        }
        private void SelectTheme(ResourceDictionary theme)
        {
            Resource.MergedDictionaries.Clear();
            Resource.MergedDictionaries.Add(theme);
            gui?.EditBarColor((Color)theme["PanelDeviceColor"]);
        }
    }
    public interface UIThemeEditable
    {
        void EditBarColor(Color color);
    }
}
