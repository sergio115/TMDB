using System;
using System.Collections.Generic;
using System.Text;
using TMDB.Utils;

namespace TMDB
{
    public class Constants
    {
        #region Movies
        public static string BaseAddres = "https://api.themoviedb.org/3/movie";
        public static string APIKey = UserSecretsManager.Settings["Movies:API_KEY"];
        public static string Language = "es-MX";
        public static string TopRated = "/top_rated";
        public static string UpComing = "/upcoming";
        public static string Pupular = "/popular";
        public static string Credits = "/credits";
        #endregion

        #region Images
        public static string BaseAddresImg = "https://image.tmdb.org/t/p";
        public static string Size = "/w500";
        public static string MinimumSize = "/w200";
        public static string OriginalSize = "/original";
        #endregion
    }
}
