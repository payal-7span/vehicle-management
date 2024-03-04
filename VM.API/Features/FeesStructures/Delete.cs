using VM.API.Common.Base;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Exceptions;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesStructures
{
    public static class Delete
    {
        public class DeleteFeesStructureCommand : BaseModifiableCommand, ICommand
        {
        }


        public class Handler(IFeesStructureRepository feesStructureRepository) : ICommandHandler<DeleteFeesStructureCommand>
        {
            private readonly IFeesStructureRepository _feesStructureRepository = feesStructureRepository;
            public async Task<ApiResult> Handle(DeleteFeesStructureCommand command, CancellationToken token)
            {

                var feesStructure = await _feesStructureRepository.GetAsync(command.Id);

                if (feesStructure == null || feesStructure.IsDeleted)
                {
                    throw new BadRequestException("FeesStructure_NotExist");
                }

                await _feesStructureRepository.RemoveAsync(feesStructure);

                return ApiResult.SuccessResult();
            }
        }
    }
}
