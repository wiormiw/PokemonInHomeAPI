using PokemonInHomeAPI.Application.Common.Interfaces;
using PokemonInHomeAPI.Application.Common.Security;

namespace PokemonInHomeAPI.Application.Moves.Queries.GetMoves;

[Authorize]
public record GetMoveQuery : IRequest<MoveDto>
{
    public int MoveId { get; init; }
}

public class GetMoveDetailQueryHandler : IRequestHandler<GetMoveQuery, MoveDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMoveDetailQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MoveDto> Handle(GetMoveQuery request, CancellationToken cancellationToken)
    {
        var move = await _context.Moves
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == request.MoveId, cancellationToken);

        if (move is null)
        {
            throw new NotFoundException(nameof(Moves), $"{request.MoveId}");
        }
        
        return _mapper.Map<MoveDto>(move);
    }
}
