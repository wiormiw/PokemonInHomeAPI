


    using Microsoft.AspNetCore.Http.HttpResults;
    using PokemonInHomeAPI.Application.Pokemons.Commands.AcceptTrade;
    using PokemonInHomeAPI.Application.Pokemons.Commands.CatchPokemon;
    using PokemonInHomeAPI.Application.Pokemons.Commands.OfferTrade;

    namespace PokemonInHomeAPI.Web.Endpoints;

    public class PokemonGame : EndpointGroupBase
    {
        public override void Map(WebApplication app)
        {
            app.MapGroup(this)
                .RequireAuthorization()
                .MapPost(CatchPokemon, "/catch")
                .MapPost(OfferTradePokemon, "/offer-trade")
                .MapPost(AcceptTradePokemon, "/accept-trade");
        }

        public async Task<Created<int>> CatchPokemon(ISender sender, CatchPokemonCommand command)
        {
            var id = await sender.Send(command);

            return TypedResults.Created($"/{nameof(PokemonGame)}/{id}", id);
        }
        
        public async Task<Ok<int>> OfferTradePokemon(ISender sender, OfferTradeCommand command)
        {
            var id = await sender.Send(command);
            
            return TypedResults.Ok(id);
        }

        public async Task<Ok<string>> AcceptTradePokemon(ISender sender, AcceptTradeCommand command)
        {
            var acceptedStatus = await sender.Send(command);
            
            return TypedResults.Ok(acceptedStatus);
        }
    }
