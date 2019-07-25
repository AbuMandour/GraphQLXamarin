using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Transitionals
{
    public class NextQuestionApi
    {
        [JsonProperty("bonusId")]
        public object BonusId { get; set; }

        [JsonProperty("questionLocalizedText")]
        public string QuestionLocalizedText { get; set; }

        [JsonProperty("answers")]
        public List<AnswerApi> Answers { get; set; }

        [JsonProperty("correctReplyLocalizedText")]
        public string CorrectReplyLocalizedText { get; set; }

        [JsonProperty("wrongReplyLocalizedText")]
        public string WrongReplyLocalizedText { get; set; }
    }
}
