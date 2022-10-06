using HellowWorldApi.Data.Entities;

namespace HellowWorldApi.Models
{
    public class PersonResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public Person? Data { get; set; }

    }
}
