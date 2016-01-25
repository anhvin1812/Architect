using System.ComponentModel.DataAnnotations.Schema;

namespace Architect.Entities
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState State { get; set; }
    }
}
