using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPTB2.Shared.Domain
{
    public class Reviews : BaseDomainModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
    }
}
