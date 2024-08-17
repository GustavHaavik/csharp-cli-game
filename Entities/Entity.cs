using csharp_cli_game.Components;

namespace csharp_cli_game.Entities;

public class Entity
{
    private readonly Dictionary<Type, Component> components = [];

    public void AddComponent<T>(T component) where T : Component => components[typeof(T)] = component;

    public T? GetComponent<T>() where T : Component => components.TryGetValue(typeof(T), out var component) ? (T)component : null;
}