using Domain.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Image : DomainBase
    {
        public string ImgURL { get; set; }
        public string? ProjectId { get;  set; }
        public string ImgCaption { get; set; }

        private Image() { }
        public Image(string imgURL, string imgCaption)
        {
            if (string.IsNullOrWhiteSpace(imgURL))
                throw new ArgumentException("Url cannot be empty");

            ImgURL = imgURL;
            ImgCaption= imgCaption;
        }
    }
}
