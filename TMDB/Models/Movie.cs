using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TMDB.Models
{
    public class Movie
    {
        public long Id { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }         
        
        [JsonProperty("backdrop_path")]
        public string ImageUrl { get; set; }         
        
        [JsonProperty("vote_average")]
        public float VoteAverage { get; set; }

        public int Starts { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        public List<Character> Characters { get; set; }

        [JsonProperty("production_companies")]
        public List<ProductionCompany> ProductionCompanies { get; set; }

        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }
    }

    public class Character
    {
        [JsonProperty("profile_path")]
        public string ImageProfile { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ProductionCompany
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }    
    
    public class Genre
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
