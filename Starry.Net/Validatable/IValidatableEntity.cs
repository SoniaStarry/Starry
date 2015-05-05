using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Starry.Net.Validatable
{
    public interface IValidatableEntity
    {
        string SecurityClass { set; get; }
        int Version { set; get; }
    }
}
