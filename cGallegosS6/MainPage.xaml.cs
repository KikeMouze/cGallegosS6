using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace cGallegosS6
{
    public partial class MainPage : ContentPage
    {
        private string Url = "http://192.168.17.61/ws_uisrael/post.php";
        private HttpClient client = new HttpClient();
        private ObservableCollection<Estudiante> post;
        public MainPage()
        {
            InitializeComponent();
            ObtenerDatos();
        }
        public async void ObtenerDatos()
        {
            var contenido = await client.GetStringAsync(Url);
            List<Estudiante> listaPost = JsonConvert.DeserializeObject<List<Estudiante>>(contenido);
            post = new ObservableCollection<Estudiante>(listaPost);
            listaEstudiantes.ItemsSource = post;
        }

        private void btnInsertar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Insertar());
        }

        private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var objeto = (Estudiante)e.SelectedItem;
            var codigoTem = objeto.codigo.ToString();
            int codigo = Convert.ToInt32(codigoTem);
            string nombre = objeto.nombre.ToString();
            string apellido = objeto.apellido.ToString();
            var edadTemp = objeto.edad.ToString();
            int edad = Convert.ToInt32(edadTemp);

            Navigation.PushAsync(new ActualizarEliminar(codigo, nombre, apellido, edad));
        }
    }
}