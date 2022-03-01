using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backEnd.Models
{
    public class PivoHrana
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }
        [Required]
        [Range(10, 10000)]
        public float Cena { get; set; }
        [Required]
        public bool PiceIliHrana { get; set; }
        public List<Narudzbina> Narudzbine { get; set; }
        public Meni Meni { get; set; }

    }
}