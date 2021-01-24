using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webprojekt_Simma_Fitnessstudio.Models
{
    public class Message
    {
        public String Header { get; set; }
        public String Messagetext { get; set; }
        public String Solution { get; set; }

        public Message() : this("", "", "") { }

        public Message(String header, String messagetext, String solution = "")
        {
            this.Header = header;
            this.Messagetext = messagetext;
            this.Solution = solution;
        }
    }
}
