
using DavidSerb.DataModel;
using DavidSerb.DataModel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.CorrelationService
{
    public abstract class BaseCorrelationService<T>
    {
        // BEFORE:
        //protected readonly SystemDataSet DataSet;

        //protected BaseCorrelationService(SystemDataSet dataSet)
        //{
        //    this.DataSet = dataSet;
        //}
        // AFTER:
        protected readonly AppDbContext DbContext;
        protected BaseCorrelationService(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract T CorrelateData();
    }
}
