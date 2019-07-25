using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;

namespace GraphQLDemo.Models
{
    public class Quiz : BaseModel
    {
        public string UserName { get; set; }
        public string QuestionText { get; set; }
        public bool Correct { get; set; }
        public int Porints { get; set; }
    }
}
