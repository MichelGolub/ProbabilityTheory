using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PrTh.Application.Common.Exceptions;
using PrTh.Application.Interfaces;
using PrTh.Domain;

namespace PrTh.Application.Features.Exercises.Queries.GetExerciseDetails
{
    public class GetExerciseDetailsQueryHandler
        : IRequestHandler<GetExerciseDetailsQuery, ExerciseDetailsVm>
    {
        private readonly IPrThDbContext _context;
        private readonly IMapper _mapper;

        public GetExerciseDetailsQueryHandler
            (IPrThDbContext context, IMapper mapper) => 
            (_context, _mapper) = (context, mapper);

        public async Task<ExerciseDetailsVm> Handle(GetExerciseDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var _ = await _context.Languages
                .FirstOrDefaultAsync(l => l.Id == request.LanguageId, cancellationToken)
                ?? throw new NotFoundException(nameof(Language), request.LanguageId);

            var exercise = await _context.Exercises
                .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Exercise), request.Id);

            var steps = await _context.Steps
                .Where(s => s.ExerciseId == request.Id)
                .OrderBy(s => s.Number)
                .ToListAsync(cancellationToken);
            var stepDtos = new List<StepDto>();
            foreach (var step in steps)
            {
                var stepTranslation = await _context.StepTranslations
                    .FirstOrDefaultAsync(st => st.StepId == step.Id
                        && st.LanguageId == request.LanguageId, cancellationToken);
                if (stepTranslation == null)
                {
                    stepTranslation = new StepTranslation
                    {
                        LanguageId = request.LanguageId,
                        StepId = step.Id,
                        DescriptionTranslation = "-"
                    };
                    _context.StepTranslations.Add(stepTranslation);
                }

                var dto = _mapper.Map<StepDto>(stepTranslation);
                stepDtos.Add(dto);
            }

            var translation = await _context.ExerciseTranslations
                .FirstOrDefaultAsync(t => t.ExerciseId == request.Id
                    && t.LanguageId == request.LanguageId,
                    cancellationToken);
            if (translation == null)
            {
                translation = new ExerciseTranslation
                {
                    ExerciseId = exercise.Id,
                    LanguageId = request.LanguageId,
                    DescriptionTranslation = "-"
                };
                _context.ExerciseTranslations.Add(translation);
            }

            var exerciseVm = _mapper.Map<ExerciseDetailsVm>(exercise);
            exerciseVm.Steps = stepDtos;
            exerciseVm.Description = translation.DescriptionTranslation;

            return exerciseVm;
        }
    }
}
