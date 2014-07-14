using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate.Contracts
{
    public interface INFeatureLogic
    {
        bool IsEnabled();
    }
}
