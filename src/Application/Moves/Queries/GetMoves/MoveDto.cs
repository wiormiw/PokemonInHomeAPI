using PokemonInHomeAPI.Domain.Entities;
using PokemonInHomeAPI.Domain.ValueObjects;

namespace PokemonInHomeAPI.Application.Moves.Queries.GetMoves;

public class MoveDto
{
    public string Name { get; init; } = string.Empty;
    
    public PokemonType Type { get; init; } = PokemonType.Unknown;
    
    public int Power { get; init; }
    
    public int Accuracy { get; init; }

    public int Pp { get; init; }
    
    public ICollection<PokemonMove> PokemonMoves { get; init; } = new List<PokemonMove>();
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Move, MoveDto>();
        }
    }
}
