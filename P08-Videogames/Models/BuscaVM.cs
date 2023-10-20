using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace P08_Videogames.Models
{
    public class BuscaVM
    {
        //Recuperar informacion de la base de datos
        //en forma de lista
        public List<Gamez> Games { get; set; }
        public SelectList Rati { get; set; }
        public SelectList Plat { get; set; }
        public SelectList Players { get; set;}
        public SelectList Gen { get; set; }
        public string BTitulo { get; set; }
        public string BRati { get; set; }
        public string BPlat { get; set; }
        public string BPlayers { get; set; }
        public string BGen { get; set; }

    }
}
