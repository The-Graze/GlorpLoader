using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlorpLoader
{
    public class GlorpMod : Attribute
    {
        public string GUID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }

        public GlorpMod(string GUID, string Name, string Version)
        {
            this.GUID = GUID;
            this.Name = Name;
            this.Version = Version;
        }
    }
}
