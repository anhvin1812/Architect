using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architect.Entities
{
    public abstract class EntityBase: IObjectState
    {
        [NotMapped]
        public ObjectState State { get; set; }
    }
}
