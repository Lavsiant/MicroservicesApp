using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Models
{
    public class ServiceValueResult<TModel> : ServiceResult
    {
        public TModel Model { get; }

        protected ServiceValueResult(TModel model) : base(true)
        {
            Model = model;
        }
        public ServiceValueResult(IEnumerable<string> errors) : base(errors) { }

        public new static ServiceValueResult<TModel> Success(TModel model)
        {
            return new ServiceValueResult<TModel>(model);
        }

        public new static ServiceValueResult<TModel> Failed(params string[] errors)
        {
            return new ServiceValueResult<TModel>(errors)
            {
                Succeeded = false,
                Error = string.Join('\n', errors)
            };
        }
    }
}
