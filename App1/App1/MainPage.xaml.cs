using App1.Interfaces_;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
 
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


 

namespace App1
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitGeoPlugin();

  
        }
       
        private async void InitGeoPlugin(){

            if (!CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("Advertencia ! ", "PORFAVOR ACITVE SUS DATOS PARA CONTINUAR ", "Ok");


            }
            if (!CrossGeolocator.IsSupported) {
                await DisplayAlert("Error", "Ha habido un error con el plugin", "ok");
                
            }

            if (!CrossGeolocator.Current.IsGeolocationEnabled || !CrossGeolocator.Current.IsGeolocationAvailable) {
                await DisplayAlert("Activar Localizacion", "Active su GPS por favor", "ok");
              
            }
            CrossGeolocator.Current.PositionChanged += CurrentPositionUpdate;
            await CrossGeolocator.Current.StartListeningAsync(new TimeSpan(0, 0, 2), 0.1);//time span y distancia  para escuchar

        }



        private async void CurrentPositionUpdate(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            if (!CrossGeolocator.Current.IsListening)
            {

                return;
            }
            try
            {
                var pos = await  CrossGeolocator.Current.GetPositionAsync();
                Latit.Text = pos.Latitude.ToString();
                Longit.Text = pos.Longitude.ToString();
                Altit.Text = pos.Altitude.ToString();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private   void Button_Clicked(object sender, EventArgs e)
        {
          
            Navigation.PushAsync(new Map());
        }

        private void Button_Quit(object sender, EventArgs e)
        {

            var closer = DependencyService.Get<ICloseApplication>();
            closer?.closeApplication();
        }

        //
    }
}
