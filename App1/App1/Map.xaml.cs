
namespace App1
{
    using Plugin.Connectivity;
    using Plugin.Geolocator;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Net.Http;

    using Xamarin.Forms;
    using Xamarin.Forms.Maps;
    using Xamarin.Forms.Xaml;
    using Newtonsoft.Json;
    using App1.Models_;
    using System.Collections.ObjectModel;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Map : ContentPage
    {
        public Map()
        {
            InitializeComponent();

        }
        ObservableCollection<Result> results = new ObservableCollection<Result>();
        public ObservableCollection<Result> Employees { get { return results; } }

        protected override async void OnAppearing()
        {

            base.OnAppearing();



            InitializeComponent();

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Advertencia ! ", "PORFAVOR ACITVE SUS DATOS PARA CONTINUAR ", "Ok");


            }
            var pos = CrossGeolocator.Current.GetPositionAsync();
            var lon = pos.Result.Latitude;
            var lat = pos.Result.Longitude;


            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius
            (new Position(lon, lat),
             Distance.FromKilometers(8)));


            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://maps.googleapis.com/maps/api/place/textsearch/json?query=schools&location=" + lon + "," + lat + "&radius=10000&key=AIzaSyDZX8CProrbv8hTnGK5i4a_yQHt3q23IBE");


            string xjson = await response.Content.ReadAsStringAsync();
            var json_ = JsonConvert.DeserializeObject<GMapsApiCall>(xjson);

            var x = json_;
            var xc = x.results.Count;

            if (xc == 0)
            {
                await DisplayAlert("Advertencia ! ", "No hay escuelas cerca", "Ok");

            }

            

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(lon, lat),
                Label = "Mi ubicacion",
                Address = "aqui se encuentra usted",

            };

            pin.Clicked += async (sender, e) =>
            {
                await DisplayAlert(pin.Label, "" + pin.Address, "Cancel");
            };

        


            MyMap.Pins.Add(pin);
         

            var response_g = await client.GetAsync("https://maps.googleapis.com/maps/api/place/textsearch/json?query=schools&location=" + lon + "," + lat + "&radius=10000&key=AIzaSyDZX8CProrbv8hTnGK5i4a_yQHt3q23IBE");

            switch (response_g.StatusCode)
            {
                case (System.Net.HttpStatusCode.OK):


                    string xjson_g = await response_g.Content.ReadAsStringAsync();
                    var json_g = JsonConvert.DeserializeObject<GMapsApiCall>(xjson_g);

                    var xg = json_g;
                    var xcg = xg.results.Count;

                    if (xcg == 0)
                    {
                        await DisplayAlert("Advertencia ! ", "No hay escuelas cerca", "Ok");

                    }
                    else
                    {

                        //ObservableCollection<Result> rnames = new ObservableCollection<Result>();
                        foreach (Result r in x.results)
                        {
                            Pin pinx = new Pin
                            {
                                Type = PinType.Place,
                                Position = new Position(r.geometry.location.lat, r.geometry.location.lng),
                                Label = r.name,
                                Address = r.formatted_address,

                            };


                            

                            pinx.Clicked += async (sender, e) =>
                            {
                                await DisplayAlert(pinx.Label, "The address is: " + pinx.Address, "Cancel");
                                Navigation.PushAsync(new Page1(r.geometry.location.lat, r.geometry.location.lng,r.place_id));

                            };

                            MyMap.Pins.Add(pinx);
                            var nm = r.name;

                            //rnames += r.name;
                            results.Add(new Result() { name = r.name ,place_id = r.place_id  });


                        }

                        ListSKulls.ItemsSource = results;

                    }



                    break;
                default:

                    await DisplayAlert("Advertencia ! ", "Error al cargar el mapa ", "Ok");
                    break;
            }



        }

        private async Task ListSKulls_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            await DisplayAlert("Advertencia ! ", "Tapped! ", "Ok");

        }
    }
}