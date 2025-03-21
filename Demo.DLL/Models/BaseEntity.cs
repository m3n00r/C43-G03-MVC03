using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DLL.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }//pk
        public int CreatedBy { get; set; }//user id
        public DateTime? CreatedOn { get; set; }//user on

        public int LastModifedBy { get; set; }//user id
        public DateTime? LastModifedOn { get; set; }
        public bool IsDeleted { get; set; }//soft Delete
    }
}
