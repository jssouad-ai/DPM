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

        public void AddImage(string url)
        {
            _Projectimages.Add(new Image(url));
        }
    }
}
