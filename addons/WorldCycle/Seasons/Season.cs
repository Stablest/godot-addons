using Godot;

namespace Addons.addons.WorldCycle.Seasons;

[GlobalClass]
public partial class Season : Resource
{
    [Export] public string Name { get; private set; } = "";
    [ExportGroup("Season Days")] 
    [Export] public uint DayOfStart { get; private set; }
    [Export] public uint DayOfEnd { get; private set; }
    
    public bool IsWithinSeason(uint day)
    {
        return day >= DayOfStart && day < DayOfEnd;
    }
}