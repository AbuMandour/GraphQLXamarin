using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Transitionals
{
    public class LimitsApi
    {
        [JsonProperty("questionLimit")]
        public long QuestionLimit { get; set; }

        [JsonProperty("dailyQuestionsAnswered")]
        public long DailyQuestionsAnswered { get; set; }

        [JsonProperty("dailyQuestionsAnsweredSms")]
        public long DailyQuestionsAnsweredSms { get; set; }
    }
}
