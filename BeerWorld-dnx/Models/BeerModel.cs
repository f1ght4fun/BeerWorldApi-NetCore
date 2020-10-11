using BeerWorld.Attributes;
using BeerWorld.Enums;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BeerWorld.Models
{
    public class BeerModel : BaseModel
    {
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        [JsonProperty("type")]
        [EnumExists(typeof(BeerType))]
        public BeerType BeerType { get; set; }

        [JsonProperty("rating")]
        [Range(0, 5)]
        public int? Rating { get; set; }
    }
}
