using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc.CQRS.QueryCommandClasses
{
    public class GetQuery:IRequest<List<string>>
    {

    }
}
