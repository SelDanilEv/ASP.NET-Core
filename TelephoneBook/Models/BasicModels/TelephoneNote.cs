using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelephoneBook.Models.BasicModels
{
    [Serializable]
    public class TelephoneNote
    {
        public TelephoneNote()
        {
        }

        public TelephoneNote(int id,string name, string phone)
        {
            Id = id;
            Name = name;
            Phone = phone;
        }

        public string Name { get; set; }
        public string Phone { get; set; }
        public int Id { get; set; }

    }
}
