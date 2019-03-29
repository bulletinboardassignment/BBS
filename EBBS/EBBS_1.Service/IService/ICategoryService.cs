using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;

namespace EBBS_1.Service.IService
{
    public interface ICategoryService
    {
        IEnumerable<Categories> LastCategory { get; }
        void Save(Categories category);
        void IncreaseFreqOne(int Id);
        void DecreaseFreqOne(int Id);
        IEnumerable<Categories> CategoryIEnum { get; }
        IQueryable<Categories> CategoryIList { get; }
        Categories Details(int? Id);
        Categories Delete(int? Id);
    }
}
