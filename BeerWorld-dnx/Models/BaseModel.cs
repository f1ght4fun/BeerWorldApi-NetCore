using BeerWorld.Interfaces;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BeerWorld.Models
{
    public class BaseModel : IModel
    {
        [JsonProperty("id")]
        [Required]
        public Guid Id { get; set; }
    }
}
