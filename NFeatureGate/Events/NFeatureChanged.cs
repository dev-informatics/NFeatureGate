using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFeatureGate.Events
{

    public class NFeatureChangedEventArgs : EventArgs
    {
        public string Name { get; private set; }

        public NFeatureChangedEventArgs(NFeature feature)
        {
            Name = feature.Name;
        }
    }

    public delegate void NFeatureChangedEventHandler(NFeature sender, NFeatureChangedEventArgs args);

}
