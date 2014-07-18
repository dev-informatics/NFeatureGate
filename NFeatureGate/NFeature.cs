using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NFeatureGate.Contracts;
using NFeatureGate.Events;

namespace NFeatureGate
{
    public class NFeature
    {
        public event NFeatureChangedEventHandler NameChanged; 

        private string _name;

        public string Name 
        { 
            get 
            { 
                return _name; 
            }
 
            set 
            {
                var args = new NFeatureChangedEventArgs(this);
                _name = value.Trim();
                OnNameChanged(args);
            } 
        }

        protected virtual void OnNameChanged(NFeatureChangedEventArgs args)
        {
            if (NameChanged != null) NameChanged(this, args);
        }
    }
}
