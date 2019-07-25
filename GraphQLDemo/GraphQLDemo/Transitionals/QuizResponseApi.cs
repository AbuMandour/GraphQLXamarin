using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraphQLDemo.Transitionals
{
    public class QuizResponseApi
    {
        [JsonProperty("lastQuestionResult")]
        public object LastQuestionResult { get; set; }

        [JsonProperty("nextQuestion")]
        public NextQuestionApi NextQuestion { get; set; }
    }
}
