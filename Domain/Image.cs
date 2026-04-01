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
        public int ProjectId { get;  set; }
        public string ImgCaption { get; set; }

        private Image() { }
        public Image(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("Url cannot be empty");

            ImgURL = url;
        }
    }
}
