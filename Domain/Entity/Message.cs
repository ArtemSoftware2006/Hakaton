using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Message
    {
        public int Id { get; set; }
        public string Text{ get; set; }
        public DateTime Published { get; set; }

    }
}
