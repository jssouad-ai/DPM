using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ImageDTO: DomainBaseDTO
    {
        [Required]
        public string ImgURL { get; set; }
        public string? ProjectId { get; set; }
        public string ImgCaption { get; set; }

        private ImageDTO() { }
        public ImageDTO(string imgURL, string imgCaption)
        {
           /* if (string.IsNullOrWhiteSpace(imgURL))
                throw new ArgumentException("Url cannot be empty");*/

            ImgURL = imgURL;
            ImgCaption = imgCaption;
        }
    }
}
