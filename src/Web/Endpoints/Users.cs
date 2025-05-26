using PokemonInHomeAPI.Infrastructure.Identity;

namespace PokemonInHomeAPI.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        var group = app.MapGroup(this);

        group.MapIdentityApiCustom<ApplicationUser>();

        // group.MapPost("/register", async (
        //         [FromBody] RegisterRequest request,
        //         UserManager<ApplicationUser> userManager,
        //         RoleManager<IdentityRole> roleManager) =>
        //     {
        //         var user = new ApplicationUser
        //         {
        //             UserName = request.Email,
        //             Email = request.Email
        //         };
        //
        //         var result = await userManager.CreateAsync(user, request.Password);
        //
        //         if (!result.Succeeded)
        //         {
        //             return Results.BadRequest(result.Errors);
        //         }
        //
        //         // Ensure the role exists before assigning
        //         var roleAssign = new[] { Roles.User, Roles.Player };
        //
        //         foreach (var role in roleAssign)
        //         {
        //             if (!await roleManager.RoleExistsAsync(role))
        //             {
        //                 await roleManager.CreateAsync(new IdentityRole(role));
        //             }
        //
        //             await userManager.AddToRoleAsync(user, role);   
        //         }
        //
        //         return Results.Ok("User registered with role 'User'");
        //     })
        //     .AllowAnonymous()
        //     .WithName("CustomRegister");
    }
}
