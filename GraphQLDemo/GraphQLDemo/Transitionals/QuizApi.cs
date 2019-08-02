using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Transitionals
{
    public class QuizApi : WhiteMvvm.Bases.BaseTransitional
    {
        [JsonProperty("responseCode")]
        public long ResponseCode { get; set; }

        [JsonProperty("profile")]
        public ProfileApi Profile { get; set; }

        [JsonProperty("quizResponse")]
        public QuizResponseApi QuizResponse { get; set; }        
    }
}
