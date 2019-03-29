using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;

namespace EBBS_1.Service.IService
{
    public interface IImageService
    {
        bool Save(Images image);
        IEnumerable<Images> ImageIEnum { get; }
        IQueryable<Images> ImageList { get; }
        Images Delete(int? Id);
        Images Details(int? Id);
    }
}
