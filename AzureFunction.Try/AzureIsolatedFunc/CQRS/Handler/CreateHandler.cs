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
    public class CreateHandler : IRequestHandler<AddCommand, string>
    {
        private ICrudRepo _repo;

        public CreateHandler(ICrudRepo repo)
        {
            _repo = repo;
        }
        public async Task<string> Handle(AddCommand request, CancellationToken cancellationToken)
        {
            TestModel model = new TestModel();
            model.Id = request.Id;
            model.Name = request.Name;  
            model.Description = request.Description;

            //called repo
            return await _repo.AddData(model);
        }
    }
}
