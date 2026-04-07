using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ProjectDTO : DomainBaseDTO
    {
        [Required]
        public string ProjectName { get; set; }

        [Required]
        public string ProjectDescription { get; set; }

        [Required]
        public string CategoryId { get; set; }

        private readonly List<ImageDTO> _Projectimages = new();
        public IReadOnlyCollection<ImageDTO> Images => _Projectimages;

        public void AddImage(string url, string caption)
        {
            _Projectimages.Add(new ImageDTO(url, caption));
        }
        public void AddImage(ImageDTO img)
        {
            _Projectimages.Add(img);
        }
    }
}
