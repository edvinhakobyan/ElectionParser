using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionParser
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Region { get; set; }
        public string StreetAndBuilding { get; set; }

        public override string ToString()
        {
            string date = DateOfBirth == null ? "null" : DateOfBirth.ToString();
            return $"{Name}|{Surname}|{MiddleName}|{date}|{Region}|{StreetAndBuilding}";
        }
    }


}
