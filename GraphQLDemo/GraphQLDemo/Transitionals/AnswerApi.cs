using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Transitionals
{
    public class AnswerApi
    {
        [JsonProperty("questionAnswerLocalizedText")]
        public string QuestionAnswerLocalizedText { get; set; }

        [JsonProperty("answerReply")]
        public long AnswerReply { get; set; }

        [JsonProperty("correct")]
        public bool Correct { get; set; }

        [JsonProperty("points")]
        public long Points { get; set; }
    }
}
