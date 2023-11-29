using Application.Abstractions.Common;
using Application.Abstractions.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.TestFeature;

public class GetStaticDataQuery : IQuery<string>
{
    internal class GetStaticDateQueryHandler : IRequestHandler<GetStaticDataQuery, string>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetStaticDateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<string> Handle(GetStaticDataQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var data = await _unitOfWork.User.Queryable
                    .Select(x => x.LoginName)
                    .FirstOrDefaultAsync();            
                return data;

            }
            catch (Exception ex)
            {
                return "Error Occured";                
            }
            
        }
    }
}
