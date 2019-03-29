using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EBBS_1.Data;

namespace EBBS_1.Service.IService
{
    public interface IPostService
    {
        Task SaveAsync(Posts post);
        IEnumerable<Posts> PostIEnum { get; }
        IEnumerable<Posts> LastPost { get; }

        Posts Details(int? Id);
        IQueryable<Posts> PostList { get; }

        Posts Delete(int? Id);
        //To increase number of commnet one after add new comment
        void IncreaseFreqOne(int Id);
        void DecreaseFreqOne(int Id);
    }
}
