using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesHeads
{
    public static class Delete
    {
        public class DeleteFeesHeadCommand : BaseModifiableCommand, ICommand
        {
        }


        public class Handler(IFeesHeadRepository feesHeadRepository) : ICommandHandler<DeleteFeesHeadCommand>
        {
            private readonly IFeesHeadRepository _feesHeadRepository = feesHeadRepository;
            public async Task<ApiResult> Handle(DeleteFeesHeadCommand command, CancellationToken token)
            {

                var feesHead = await _feesHeadRepository.GetAsync(command.Id);

                if (feesHead == null || feesHead.IsDeleted)
                {
                    throw new BadRequestException("FeesHead_NotExist");
                }

                await _feesHeadRepository.RemoveAsync(feesHead);

                return ApiResult.SuccessResult();
            }
        }
    }
}
