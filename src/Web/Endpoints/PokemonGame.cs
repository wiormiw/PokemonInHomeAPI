


    using Microsoft.AspNetCore.Http.HttpResults;
    using Microsoft.Extensions.DependencyInjection.Pokemons.Commands.CatchPokemon;

    namespace PokemonInHomeAPI.Web.Endpoints;

    public class PokemonGame : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapPost(CatchPokemon, "/catch");
        }

        public async Task<Created<int>> CatchPokemon(ISender sender, CatchPokemonCommand command)
        {
            var id = await sender.Send(command);

            return TypedResults.Created($"/{nameof(PokemonGame)}/{id}", id);
        }
    }
