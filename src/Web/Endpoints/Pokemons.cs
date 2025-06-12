using PokemonInHomeAPI.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using PokemonInHomeAPI.Application.Pokemons.Commands.CreatePokemon;
using PokemonInHomeAPI.Application.Pokemons.Commands.DeletePokemon;
using PokemonInHomeAPI.Application.Pokemons.Commands.UpdatePokemon;
using PokemonInHomeAPI.Application.Pokemons.Queries.GetPokemons;

namespace PokemonInHomeAPI.Web.Endpoints;

public class Pokemons : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetPokemonsWithPagination)
            .MapGet(GetPokemonById, "{id}")
            .MapPost(CreatePokemon)
            .MapPut(UpdatePokemon, "{id}")
            .MapDelete(DeletePokemon, "{id}");
    }

    public async Task<Ok<PaginatedList<PokemonDto>>> GetPokemonsWithPagination(ISender sender,
        [AsParameters] GetPokemonsWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
    
    public async Task<Ok<PokemonDto>> GetPokemonById(ISender sender, int id)
    {
        var result = await sender.Send(new GetPokemonQuery{ PokemonId = id });

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreatePokemon(ISender sender, CreatePokemonCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(Pokemons)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdatePokemon(ISender sender, int id,
        UpdatePokemonCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeletePokemon(ISender sender, int id)
    {
        await sender.Send(new DeletePokemonCommand(id));

        return TypedResults.NoContent();
    }
}
