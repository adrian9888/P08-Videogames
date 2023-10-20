using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P08_Videogames.Models
{
    public class Gamez
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Genre { get; set; }
        [DataType(DataType.Date)]
        public DateTime Release { get; set; }
        public string Publisher { get; set; }
        public string Developer { get; set; }
        public string Platform { get; set; }
        //Restringir a un rango de jugadores
        public string Players { get; set; }
        public string Rating { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName ="decimal(18,2)")]
        public decimal Price { get; set; }
        [StringLength(150,MinimumLength =10)]
        public string Description { get; set; }
    }
}
