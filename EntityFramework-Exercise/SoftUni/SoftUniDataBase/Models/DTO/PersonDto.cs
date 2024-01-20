using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SoftUni.Models.DTO
{
    public class PersonDto
    {
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AddressText { get; set; }
        public string City { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
