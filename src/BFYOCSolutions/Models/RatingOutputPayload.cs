using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BFYOCSolutions.Models
{
    public class RatingOutputPayload
    {
        public Guid id { get; set; }
        public Guid userId { get; set; }
        public Guid productId { get; set; }
        public DateTime timestamp { get; set; }
        public string locationName { get; set; }
        [Required, Range(0, 5)]
        public int rating { get; set; }
        public string userNotes { get; set; }
    }
}
