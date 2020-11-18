using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BFYOCSolutions.Ratings
{
    public class RatingInputPayload
    {
        [Required]
        public Guid userId { get; set; }
        [Required]
        public Guid productId { get; set; }
        public string locationName { get; set; }
        [Required, Range(0, 5)]
        public int rating { get; set; }
        public string userNotes { get; set; }

    }
}
