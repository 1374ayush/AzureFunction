using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc.CustomErrors
{
    public class ValidationException:Exception
    {
        public List<string> Errors { get; set; }

        public ValidationException(List<string> res):base("Error Res")
        {
            Errors = res;
        }
        
    }
}
