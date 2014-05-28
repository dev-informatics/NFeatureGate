using NFeatureGate.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate
{
    public class NFeatureState
    {
        public NFeature Feature { get; set; }
        public dynamic State { get; set; }
    }
}
