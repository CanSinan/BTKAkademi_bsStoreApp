using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    public class Link
    {
        public string? Href { get; set; } // referans bilgi, link bilgisi nereye verilecekse o amaçla kullanacağız.
        public string? Rel { get; set; } // silme , güncelleme ??
        public string? Method { get; set; } 

        public Link() 
        {

        }

        public Link(string? hRef, string? rel, string? method)
        {
            Href = hRef;
            Rel = rel;
            Method = method;
        }
    }
}
