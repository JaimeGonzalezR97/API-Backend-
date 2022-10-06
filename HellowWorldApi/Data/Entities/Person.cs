using System.ComponentModel.DataAnnotations;

namespace HellowWorldApi.Data.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        public string Speak { get; set; }
        public string Name { get; set; }
        public int Edad { get; set; }
        public double? Estatura { get; set; }    
    }
}
