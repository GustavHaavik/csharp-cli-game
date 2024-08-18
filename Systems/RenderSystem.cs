using csharp_cli_game.Components;
using csharp_cli_game.Entities;
using csharp_cli_game.Worlds;

namespace csharp_cli_game.Systems;

public class RenderSystem
{

    public void Render(Entity entity, World world)
    {
        var position = entity.GetComponent<Position>();
        var renderable = entity.GetComponent<Renderable>();

        if (position == null || renderable == null) return;

        renderable.Render(world, position);
    }
}