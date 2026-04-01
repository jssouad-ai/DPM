using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Commun
{
    public class DomainBase 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime  UpdatedDate { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DeletedDate { get; set; }

        public int IsDeleted {  get; set; }


    }
}
