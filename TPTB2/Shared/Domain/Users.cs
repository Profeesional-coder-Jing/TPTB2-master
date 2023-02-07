using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPTB2.Shared.Domain
{
    public class Users : BaseDomainModel
    {
        public string Username { get; set; }
        public string Contact { get; set; }
        public string DateOfBirth { get; set; }
        public string Email { get; set; }
        public int? PaymentsId { get; set; }
        public virtual Payments Payments { get; set; }
        public int? ReviewsId { get; set; }
        public virtual Reviews Reviews { get; set; }
    }
}
