using App1.Models_;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public   Page1(double ln, double lat, string place_id)
        {
            InitializeComponent();


            _ = CallData(place_id);

        }

        public async Task CallData(string place_id) {


            var stq = "https://maps.googleapis.com/maps/api/place/details/json?place_id="+ place_id + "&fields=name,rating,formatted_phone_number&key=AIzaSyDZX8CProrbv8hTnGK5i4a_yQHt3q23IBE";



            HttpClient client = new HttpClient();
            var response = await client.GetAsync(stq);


            string xjson = await response.Content.ReadAsStringAsync();
            var json_ = JsonConvert.DeserializeObject<PlaceDetails>(xjson);

            var x = json_;


            //img.Source = x.results[0].photos.;
            formatted_address.Text = x.result.formatted_phone_number;
            name_N.Text = x.result.name;
            var open = x.result.opening_hours.open_now;
            if (open)
            {
                opening_hours.Text = "Abierto ahora";
            }

            rating.Text = x.result.rating.ToString();
        }
    }
}