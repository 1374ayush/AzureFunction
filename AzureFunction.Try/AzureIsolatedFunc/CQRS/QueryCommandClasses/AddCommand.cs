using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc.CQRS.QueryCommandClasses
{
    public class AddCommand:IRequest<string>
    {
        public AddCommand( int id , string name , string desc) {
            Id = id;
            Name = name;
            Description = desc;
        }

        public int Id {  get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
