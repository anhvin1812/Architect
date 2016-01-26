using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architect.Entities.IdentityManagement
{
    public class Member: EntityBase
    {
        public int MemberId { get; set; }
        public string FullName  { get; set; }
        public string Email { get; set; }
    }
}
