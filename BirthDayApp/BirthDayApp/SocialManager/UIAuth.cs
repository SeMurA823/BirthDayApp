using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace BirthDayApp.SocialManager
{
    public interface UIAuth
    {        void StartAuth(OAuth2Authenticator authenticator);
    }
}
