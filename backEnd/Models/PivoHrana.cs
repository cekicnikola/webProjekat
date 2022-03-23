using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backEnd.Models
{
    public class PivoHrana
    {
        [Key]
        public int ID { get; set; }
       [Required(ErrorMessage ="Nepravilan unos Naziva!")]
        [MaxLength(50, ErrorMessage="Naziv mora imati manje od 50 karaktera!")]
        public string Naziv { get; set; }
        [Required(ErrorMessage ="Nedozvoljena vrednost cene")]
        [Range(10, 10000, ErrorMessage ="Nedozvoljena vrednost, {0} mora biti izmedju {1} i {2}!")]
        public float Cena { get; set; }
        [Required]
        public bool PiceIliHrana { get; set; }
        [JsonIgnore]
        public List<Narudzbina> Narudzbine { get; set; }
        [JsonIgnore]
        public Meni Meni { get; set; }

    }
}