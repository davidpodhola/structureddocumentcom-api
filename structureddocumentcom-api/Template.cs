using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.structureddocument.api
{
    public class Template
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CultureInfoName { get; set; }

        public Template(dynamic t)
        {
            this.Id = t.Id;
            this.Title = t.Title;
            this.Description = t.Description;
            this.CultureInfoName = t.Language().CultureInfoName;
        }
    }
}
