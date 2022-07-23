using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Models
{
    public class ServiceResult
    {
        protected ServiceResult(IEnumerable<string> errors)
        {
            if (errors == null || !errors.Any())
            {
                errors = new[] { "An error occured" };
            }
            Succeeded = false;
            Error = string.Join("\n", errors);
        }

        protected ServiceResult(bool success)
        {
            Succeeded = success;
        }

        public bool Succeeded { get; set; }

        public string Error { get; set; }

        public static ServiceResult Success { get; } = new ServiceResult(true);

        public static ServiceResult Failed(params string[] errors)
        {
            return new ServiceResult(errors);
        }
    }
}
