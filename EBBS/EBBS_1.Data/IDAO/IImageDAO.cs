using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EBBS_1.Data.IDAO
{
    public interface IImageDAO
    {
        bool Save(Images image);
        IEnumerable<Images> ImageIEnum { get; }
        IQueryable<Images> ImageList { get; }
        Images Delete(int? Id);
        Images Details(int? Id);
    }
}
