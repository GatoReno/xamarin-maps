using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models_
{

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

 

    public class PlaceDetails
    {
        public List<object> html_attributions { get; set; }
        public Result result { get; set; }
        public string status { get; set; }
    }
    
}
