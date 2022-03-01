using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backEnd.Models
{
    public class Narudzbina
    {
        [Key]
        public int ID { get; set; }
        public int KolicinaPiva { get; set; }
        public int KolicinaHrane { get; set; }
        [JsonIgnore]
        public Sto Sto { get; set; }
        public PivoHrana PivoHrana { get; set; }
        public List<Promocija> Promocije { get; set; }
        public bool Doneta { get; set; }


    }
}