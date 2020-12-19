using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkApiCore.Items;

namespace VkApiCore
{
    public class Api
    {
        public const string START_URL = "https://api.vk.com/method/";
        public const string API_VERSION = "5.126";
        public string Token { internal get; set; }
        public string AppID { get; private set; }
        public bool CheckToken(string token)
        {
            if (token == null) return false;
            Token = token;
            return GetUser().Request() != null;
        }
        public IFriendListRequest GetFriendList()
        {
            Console.WriteLine("KKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK");
            return new FriendListRequest(Token);
        }
        public IUserRequest GetUser()
        {
            return new UserRequest(Token);
        }        
    }

    public interface IFriendListRequest
    {
        IFriendListRequest Id(string id);
        IFriendListRequest SetField(UserFields field);
        List<Friend> Request();
    }
    public interface IUserRequest
    {
        IUserRequest SetField(UserFields field);
        User Request();
    }
    abstract class ApiRequest
    {
        string access_token;
        protected ApiRequest(string token)
        {
            access_token = token;
        }
        protected T GetResponse<T>(string url)
        {
            string request = url +
                "access_token=" +
                
                "&v=" +
                Api.API_VERSION;
            Console.WriteLine(request);
            string response = CreateRequest(request);
            Console.WriteLine(response);
            RootResponse<T> rootResponse =
                JsonConvert.DeserializeObject<RootResponse<T>>(response);
            if (rootResponse?.Error != null) throw new ApiException(rootResponse?.Error?.Msg);
            return rootResponse.Response;
        }
        private string CreateRequest(string url)
        {
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
        public class RootResponse<T>
        {
            [JsonProperty("response")] public T Response;
            [JsonProperty("error")] public Error Error;
        }
        public class ItemsResponse<T>
        {
            [JsonProperty("items")] public T Items;
        }
        public class Error
        {
            [JsonProperty("error_code")] public int Code;
            [JsonProperty("error_msg")] public string Msg;
        }

        public class ApiException : Exception
        {
            public ApiException(string msg): base(msg)
            {

            }
        }
    }
    class FriendListRequest : ApiRequest, IFriendListRequest
    {
        private const string START_URL_FRIENDS = Api.START_URL + "friends.get?";
        private List<UserFields> fields = new List<UserFields>();
        private string idUser;
        public FriendListRequest(string token) : base(token)
        {
        }
        public IFriendListRequest Id(string id)
        {
            idUser = id;
            return this;
        }

        public List<Friend> Request()
        {
            string strRequest = START_URL_FRIENDS;
            strRequest += "fields=photo_200&";
            return base.GetResponse<ItemsResponse<List<Friend>>>(strRequest).Items;
        }

        public IFriendListRequest SetField(UserFields field)
        {
            if (!fields.Contains(field)) fields.Add(field);
            return this;
        }
    }
    class UserRequest : ApiRequest, IUserRequest
    {
        private const string START_URL_USER = Api.START_URL + "users.get?";
        private List<UserFields> fields = new List<UserFields>();
        public UserRequest(string token) : base(token)
        {

        }

        public User Request()
        {
            string strRequest = START_URL_USER;
            if (fields.Count != 0)
            {
                strRequest += "fields=";
                fields.ForEach(field => strRequest += field.NameUrl + ',');
                strRequest += "id&";
            }

            User[] users;
            try
            {
                users = base.GetResponse<User[]>(strRequest);
            }
            catch (ApiException e)
            {
                return null;
            }
            return (users == null)? null: users[0];
        }

        public IUserRequest SetField(UserFields field)
        {
            if (!fields.Contains(field)) fields.Add(field);
            return this;
        }
    }

    public class UserFields
    {
        public static UserFields FirstName { get; private set; }
        public static UserFields LastName { get; private set; }
        public static UserFields BDate { get; private set; }
        public static UserFields Photo200 { get; private set; }
        public static UserFields Sex { get; private set; }
        public static UserFields ScreenName { get; private set; }

        static UserFields()
        {
            FirstName = new UserFields("first_name");
            LastName = new UserFields("last_name");
            BDate = new UserFields("bdate");
            Photo200 = new UserFields("photo_200");
            Sex = new UserFields("sex");
            ScreenName = new UserFields("screen_name");
        }
        public string NameUrl { get; private set; }
        private UserFields(string name)
        {
            NameUrl = name;
        }
    }
}
