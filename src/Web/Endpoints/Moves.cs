using PokemonInHomeAPI.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using PokemonInHomeAPI.Application.Moves.Commands.CreateMove;
using PokemonInHomeAPI.Application.Moves.Commands.DeleteMove;
using PokemonInHomeAPI.Application.Moves.Commands.UpdateMove;
using PokemonInHomeAPI.Application.Moves.Queries.GetMoves;

namespace PokemonInHomeAPI.Web.Endpoints;

public class Moves : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetMovesWithPagination)
            .MapGet(GetMoveById, "{id}")
            .MapPost(CreateMove)
            .MapPut(UpdateMove, "{id}")
            .MapDelete(DeleteMove, "{id}");
    }

    public async Task<Ok<PaginatedList<MoveDto>>> GetMovesWithPagination(ISender sender,
        [AsParameters] GetMovesWithPaginationQuery query)
    {
        var result = await sender.Send(query);

        return TypedResults.Ok(result);
    }
    
    public async Task<Ok<MoveDto>> GetMoveById(ISender sender, int id)
    {
        var result = await sender.Send(new GetMoveQuery{ MoveId = id });

        return TypedResults.Ok(result);
    }

    public async Task<Created<int>> CreateMove(ISender sender, CreateMoveCommand command)
    {
        var id = await sender.Send(command);

        return TypedResults.Created($"/{nameof(TodoItems)}/{id}", id);
    }

    public async Task<Results<NoContent, BadRequest>> UpdateMove(ISender sender, int id,
        UpdateMoveCommand command)
    {
        if (id != command.Id) return TypedResults.BadRequest();

        await sender.Send(command);

        return TypedResults.NoContent();
    }

    public async Task<NoContent> DeleteMove(ISender sender, int id)
    {
        await sender.Send(new DeleteMoveCommand(id));

        return TypedResults.NoContent();
    }
}

