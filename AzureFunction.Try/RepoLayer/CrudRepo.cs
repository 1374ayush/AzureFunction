using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public class CrudRepo : ICrudRepo
    {
        public async Task<string> AddData(TestModel model)
        {
            Console.WriteLine("Data Added");
            return "Added";
        }

        public async Task<List<string>> GetData()
        {
            List<string> data = new List<string> { "Ayush", "Piyush" };
            Console.WriteLine("Data Added");
            return data;
        }

    }
}
