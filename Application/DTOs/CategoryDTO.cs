using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CategoryDTO: DomainBaseDTO
    {
        
        [Required]
        public string CategoryName { get; set; }
    }
}
