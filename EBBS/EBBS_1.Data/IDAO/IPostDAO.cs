using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EBBS_1.Data.IDAO
{
    public interface IPostDAO
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
