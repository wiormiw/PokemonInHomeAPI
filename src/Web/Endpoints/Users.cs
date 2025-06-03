using PokemonInHomeAPI.Infrastructure.Identity;

namespace PokemonInHomeAPI.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this);

        group.MapIdentityApiCustom<ApplicationUser>();
    }
}
