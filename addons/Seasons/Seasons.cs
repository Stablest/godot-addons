using System.Linq;
using Godot;
using Godot.Collections;

namespace Addons.addons.Seasons;

[GlobalClass]
public partial class Seasons : Resource
{
    [Export] public Array<Season> Items { get; private set; } = [];
    
    public Season GetCurrentSeason(uint currentDay)
    {
        return Items.FirstOrDefault(season => season.IsWithinSeason(currentDay));
    }
}