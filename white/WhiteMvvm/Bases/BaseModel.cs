using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace WhiteMvvm.Bases
{
    public class BaseModel 
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}
