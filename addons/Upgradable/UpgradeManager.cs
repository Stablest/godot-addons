using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Addons.addons.Upgradable;

[GlobalClass]
public partial class UpgradeManager : Node
{
    private Godot.Collections.Dictionary<string, Upgrade> _upgradeDictionary = new();

    private void PopulateAndValidate()
    {
        var upgrades = GetAllUpgradesFromRoot(this);
        foreach (var upgrade in upgrades.Where(upgrade => !string.IsNullOrEmpty(upgrade.UpgradeName)))
        {
            upgrade.Validate();
            _upgradeDictionary[upgrade.Name] = upgrade;
        }
    }

    public override void _Ready()
    {
        PopulateAndValidate();
    }

    private static List<Upgrade> GetAllUpgradesFromRoot(Node root)
    {
        var upgrades = new List<Upgrade>();

        foreach (var child in root.GetChildren())
        {
            if (child is Upgrade upgrade)
            {
                upgrades.Add(upgrade);
            }

            upgrades.AddRange(GetAllUpgradesFromRoot(child));
        }

        return upgrades;
    }
    
}