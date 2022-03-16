using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backEnd.Models
{
    public class Narudzbina
    {
        [Key]
        public int ID { get; set; }
        [Range(0,150, ErrorMessage ="Nedozvoljena kolicina Piva")]
        public int KolicinaPiva { get; set; }
        [Range(0,50, ErrorMessage ="Nedozvoljena kolicina Hrane")]
        public int KolicinaHrane { get; set; }
        public float ZaNaplatu { get; set; }
        [JsonIgnore]
        public Sto Sto { get; set; }
        public PivoHrana PivoHrana { get; set; }
        [Required]
        public bool Doneta { get; set; }


    }
}