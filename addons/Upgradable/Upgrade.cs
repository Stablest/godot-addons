using Godot;

namespace Addons.addons.Upgradable;

[GlobalClass]
public partial class Upgrade : Node
{
    [Export] public string UpgradeName { get; private set; }
    [Export] public string Description { get; private set; }
    [Export] public Cost Cost { get; private set; }
    [Export] public Texture2D Image { get; private set; }
    [Export] public bool IsBought { get; private set; }

    public void Validate()
    {
        Error.AssertNotNull(UpgradeName, nameof(UpgradeName), this);
        Error.AssertNotNull(Description, nameof(Description), this);
        Error.AssertNotNull(Cost, nameof(Cost), this);
    }

    public void Buy<T>(T value)
    {
        if (Cost.CanBuy(value))
        {
            IsBought = true;
        }
    }
}