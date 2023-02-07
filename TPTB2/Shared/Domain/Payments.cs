using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPTB2.Shared.Domain
{
    public class Payments: BaseDomainModel
    {
        public string Name { get; set; }
        public string CardNumber { get; set; }
        public string SecurityCode { get; set; }
        public DateTime DateOfExpiry { get; set; }
    }
}
