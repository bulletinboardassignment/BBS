using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EBBS_1.Data.IDAO
{
    public interface ICategoryDAO
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
