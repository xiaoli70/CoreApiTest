using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6Api.Domain.Helper
{
    public class Student
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int ID { get; set; }

        [SugarColumn(IsNullable = false)]
        public string Name { get; set; }

        public int Age { get; set; }

        [SugarColumn(SerializeDateTimeFormat = "yyyy-MM-dd")]
        public DateTime Birth { get; set; }

    }
}
