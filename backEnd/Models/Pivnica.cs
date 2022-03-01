using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backEnd.Models
{
    public class Pivnica
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; } 
        public int BrojStolova { get; set; }
       
        public Meni Meni { get; set; }
        public List<Sto> Stolovi { get; set; }

    }
}