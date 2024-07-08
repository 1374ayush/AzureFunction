using AzureIsolatedFunc.CQRS.QueryCommandClasses;
using MediatR;
using RepoLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIsolatedFunc.CQRS.Handler
{
    public class GetHandler : IRequestHandler<GetQuery, List<string>>
    {
        private ICrudRepo _repo;

        public GetHandler(ICrudRepo repo)
        {
            _repo = repo;
        }
        public async Task<List<string>> Handle(GetQuery request, CancellationToken cancellationToken)
        {
            //called repo
            return await _repo.GetData();
        }
    }
}
