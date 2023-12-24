using Microsoft.AspNetCore.Http.HttpResults;

namespace TestGeodanApi.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreateAt { get; set; }
        public string UpdateAt { get; set; }
        public string CreateBy { get; set; }
        public List<Sector> Sectors { get; set; }
    }
}
