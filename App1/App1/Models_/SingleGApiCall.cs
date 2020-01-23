using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models_
{
    



    public class Candidate
    {
        public string formatted_address { get; set; }
        public string name { get; set; }
        public OpeningHours opening_hours { get; set; }
        public List<Photo> photos { get; set; }
        public int rating { get; set; }
    }

    public class SingleGApiCall
    {
        public List<Candidate> candidates { get; set; }
        public string status { get; set; }
    }
}
