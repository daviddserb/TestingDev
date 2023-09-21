
using DavidSerb.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavidSerb.Domain.CorrelationService
{
    public abstract class BaseCorrelationService<T>
    {
        protected readonly SystemDataSet DataSet;

        protected BaseCorrelationService(SystemDataSet dataSet)
        {
            this.DataSet = dataSet;
        }

        public abstract T CorrelateData();
    }
}
