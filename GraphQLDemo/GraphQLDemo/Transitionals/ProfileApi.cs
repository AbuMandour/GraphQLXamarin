using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Transitionals
{
    public class ProfileApi
    {
        [JsonProperty("msisdn")]
        public long Msisdn { get; set; }

        [JsonProperty("authKey")]
        public string AuthKey { get; set; }

        [JsonProperty("username")]
        public long Username { get; set; }

        [JsonProperty("deviceUniqueId")]
        public object DeviceUniqueId { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("registrationDate")]
        public long RegistrationDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("totalPoints")]
        public long TotalPoints { get; set; }

        [JsonProperty("weeklyPoints")]
        public long WeeklyPoints { get; set; }

        [JsonProperty("lastActive")]
        public long LastActive { get; set; }

        [JsonProperty("totalQuestionsAnswered")]
        public long TotalQuestionsAnswered { get; set; }

        [JsonProperty("questionType")]
        public string QuestionType { get; set; }

        [JsonProperty("limits")]
        public LimitsApi Limits { get; set; }
    }
}
