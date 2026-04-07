using Domain.Commun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Domain
{
    public class Project : DomainBase
    {
        public string ProjectName { get;  set; }
        public string ProjectDescription { get; set; }
        public string CategoryId { get; set; }

        private readonly List<Image> _Projectimages = new();
        public IReadOnlyCollection<Image> Images => _Projectimages;

        /* public void AddImage(string url , string caption)
         {
             _Projectimages.Add(new Image(url, caption));
         }
         public void AddImage(Image img)
         {
             _Projectimages.Add(img);
         }*/

        public void AddImage(Image image)
        {
            if (image == null) return;

            if (_Projectimages.Any(i => i.Id == image.Id))
                return;

            _Projectimages.Add(image);
        }

        public void AddImages(IEnumerable<Image > images)
        {
            foreach (var image in images)
            {
                AddImage(image);
            }
        }

        /* au lieu de .Any
         var existingUrls = _Projectimages.Select(i => i.ImgURL).ToHashSet();
          foreach (var img in images)
{
    if (existingUrls.Add(img.ImgURL)) // retourne true si URL nouvelle
    {
        _Projectimages.Add(img);
    }
}
          */

        /*   public void RemoveImage(Image img)
           {
               _Projectimages.Remove(img);
           }*/
    }
}
