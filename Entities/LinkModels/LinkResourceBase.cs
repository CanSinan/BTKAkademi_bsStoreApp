using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    public class LinkResourceBase // Linklerin organizasyonunu yapacak.
    {
        public LinkResourceBase()
        {
            
        }

        public List<Link> Links { get; set; } = new List<Link>();// tanımlandığı yerde ilgili referansı verdik.
    }
}
