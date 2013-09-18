using com.structureddocument.api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.structureddocument.client
{
    public interface ITemplateService
    {
        Task<IEnumerable<Template>> GetAll();
        Task Add(Template expense);
        Task Delete(int id);
        Task Update(Template expense);
    }
}
