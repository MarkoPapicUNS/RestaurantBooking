using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestaurantBooking.API.Models
{
    public class RatingDto
    {
        [Required]
        public string RestaurantId { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}