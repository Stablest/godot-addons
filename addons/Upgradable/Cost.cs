using Godot;

namespace Addons.addons.Upgradable;

[GlobalClass]
public abstract partial class Cost : Resource
{
    public abstract bool CanBuy<T>(T value);
}