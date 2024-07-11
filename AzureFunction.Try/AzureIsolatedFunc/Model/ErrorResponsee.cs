using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc.Model
{
    public class ErrorResponsee<T> where T : class
    {
        public string Status {  get; set; }  
        public T Message { get; set; }
    }
}
