using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backEnd.Models
{
    public class Promocija
    {
        [Key]
        public int ID { get; set; }
        public int MinKolicinaPica { get; set; }
        public int MinKolicinaHrane  { get; set; }
        public int Popust { get; set; }
        public List<Narudzbina> Narudzbine { get; set; }
        
    }
}