using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AGCalculator
{
    class ObjectInfo
    {
        public string Name { private set; get; }
        public string[] LabelNames { private set; get; }
        public string Specification { private set; get; }

        public ObjectInfo(string name, string[] parameteres, string spec)
        {
            this.Name = name;
            this.LabelNames = parameteres;
            this.Specification = spec;
        }
    }
}
