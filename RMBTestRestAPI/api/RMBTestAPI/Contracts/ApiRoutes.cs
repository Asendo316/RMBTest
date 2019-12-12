using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RMBTestRestAPI.Contracts
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Authentication
        {
            public const string RegisterUser = Base + "/user/register";
            public const string RegisterResturant = Base + "/restaurant/register";

            public const string LoginUser = Base + "/user/login";
            public const string LoginResturant = Base + "/restaurant/login";

            public const string RefreshToken = Base + "/token/refresh";

            public const string ResetPassword = Base + "/password/reset";

            public const string ResetPasswordUser = Base + "/password/reset/user";


            public const string ConfrimEmail = Base + "/email/confirm";


            public const string ChangePassword = Base + "/password/change";

        }

        public static class UserProfile
        {
            public const string GetAll = Base + "/user/profile/all";

            public const string Get = Base + "/user/profile/{profileId}";

            public const string Update = Base + "/user/profile/{profileId}";

            public const string Delete = Base + "/user/profile/{profileId}";
        }




        public static class Favourites
        {
            public const string GetAllFavouriteResturants = Base + "/favourites/restaurants/all/";

            public const string CreateLikeOnResturant = Base + "/favourites/restaurant/";

            public const string DeleteLikeOnResturant = Base + "/favourites/restaurant/{favouritesId}";

            public const string GetUserFavourites = Base + "/favourites/{userId}";
        }

        public static class Accounts
        {
            public const string GetAllAccountDetails = Base + "/accounts/details/all/";

            public const string GetAccountById = Base + "/accounts/details/{accountNumber}";

            public const string CreateAccount = Base + "/accounts/create/";
        }
    }
}
