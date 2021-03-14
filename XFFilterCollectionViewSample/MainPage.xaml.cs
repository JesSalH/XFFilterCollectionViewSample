using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFFilterCollectionViewSample
{
    public partial class MainPage : ContentPage
    {
        //1.- sourceItems = la lista original
        private readonly string[] sourceItems = new[] { "Subscribe", "YouTube Channel", "Monkeys", "Gerald", "Subscribed yet?", "another item here", "and another one" };

        public ObservableCollection<string> MyItems { get; set; }

        public ObservableCollection<string> MyCollectionItems { get; set; }

        public MainPage()
        {
            InitializeComponent();

            //2.- la lista que va a ir quedando (la lista que se va a mostrar por pantalla)
            MyItems = new ObservableCollection<string>(sourceItems);

            MyCollectionItems = new ObservableCollection<string>(sourceItems);

            BindingContext = this;
        }

        void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //ojo como coge el valor de la searchbar .NewTextValue
            var searchTerm = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = string.Empty;
            }

            searchTerm = searchTerm.ToLowerInvariant();



            //3.- filtered Items= lista filtrada que contiene los valores de búsqueda
            var filteredItems = sourceItems.Where(value => value.ToLowerInvariant().Contains(searchTerm)).ToList();


            //recorremos la lista original (1 sourceItems)
            //Hay dos casuísticas: Ir quitando de la que se muestra e ir añadiendo
            foreach (var value in sourceItems)
            {


                //PARA IR QUITANDO
                //se ese item de la lista original no esta en la lista de filtrados (3 filteredItems) se borra de la lista que va quedando (2 MyItems)
                if (!filteredItems.Contains(value))
                {
                    MyItems.Remove(value);
                }

                //PARA IR AÑADIENDO (se supone que hemos borrado muchos items y ahora la lista filtro tiene más que antes)
                //si ese item de la lista original (1 sourceItems) no esta en en la lista que va quedando (2 My Items) (PERO SÍ QUE ESTÁ EN LA LISTA DE FILTRADOS POR ELIMINACIÓN ) entonces se añade a la lista de los que van quedando (2)
                else if (!MyItems.Contains(value))
                {
                    MyItems.Add(value);
                }
            }
        }


        void SearchBarCollection_TextChanged(object sender, TextChangedEventArgs e)
        {

            var searchTerm = e.NewTextValue;

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = string.Empty;
            }

            searchTerm = searchTerm.ToLowerInvariant();

           
            var filteredItems = sourceItems.Where(value => value.ToLowerInvariant().Contains(searchTerm)).ToList();

            MyCollectionItems.Clear();

            //en lugar de modificar la coleccion en 2 pasos como antes la  limpiamos y añadimos los items de la lista filtrada
            foreach (var item in filteredItems)
            {
                MyCollectionItems.Add(item);
            }
        }
    }
}
