using FluentValidation;
using VM.API.Common.Interfaces;
using VM.API.Common.Models;
using VM.Common.Constants;
using VM.DataAccess.Entities;
using VM.DataAccess.Repositories;

namespace VM.API.Features.FeesStructures
{
	public static class Create
	{
		public class CreateFeesStructureCommand : ICommand<CreateFeesStructureResult>
		{
			public int? TypeId { get; set; }
			public int FeesHeadId { get; set; }
			public FeesStructureTypeEnum IsFixOrPercentage { get; set; }
			public double? Value { get; set; }
			public double? MinValue { get; set; }
			public double? MaxValue { get; set; }
		}

		public class CreateFeesStructureResult()
		{
			public int Id { get; set; }
		}

		public class Handler(IFeesStructureRepository feesStructureRepository) : ICommandHandler<CreateFeesStructureCommand, CreateFeesStructureResult>
		{
			private readonly IFeesStructureRepository _feesStructureRepository = feesStructureRepository;
			public async Task<ApiResult<CreateFeesStructureResult>> Handle(CreateFeesStructureCommand command, CancellationToken token)
			{
				FeesStructure newFeesStructure = new()
				{
					TypeId = command.TypeId,
					FeesHeadId = command.FeesHeadId,
					IsFixOrPercentage = command.IsFixOrPercentage.ToString(),
					Value = command.Value,
					MinValue = command.MinValue,
					MaxValue = command.MaxValue
				};

				var result = await _feesStructureRepository.CreateAsync(newFeesStructure);

				return ApiResult<CreateFeesStructureResult>.SuccessResult(new CreateFeesStructureResult() { Id = result.Id });
			}
		}

		public class Validator : AbstractValidator<CreateFeesStructureCommand>
		{
			public Validator()
			{
				RuleFor(x => x.FeesHeadId).NotEmpty();
			}
		}
	}
}
