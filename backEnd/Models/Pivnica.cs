namespace backEnd.Models
{
    public class Pivnica
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Naziv { get; set; }
        
        
    }
}