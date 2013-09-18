using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.structureddocument.api
{
    public interface ITemplateService
    {
        Task<IEnumerable<Template>> GetAll();
        Task Add(Template expense);
        Task Delete(int id);
        Task Update(Template expense);
    }
}
