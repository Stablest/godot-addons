using Godot;

namespace Addons.addons.WorldCycle.TimeCycle;

[GlobalClass]
public partial class TimeCycle : Node
{
    [Signal]
    public delegate void TimeChangedEventHandler(uint currentTime);

    [Signal]
    public delegate void CycleEndedEventHandler();

    [ExportGroup("Speed properties")]
    [Export] public uint TimeSpeed { get; private set; } = 1;
    [Export] public uint TickIntervalInSeconds { get; private set; } = 1;
    [ExportGroup("Cycle properties")]
    [Export] public uint CycleDurationInSeconds { get; private set; }
    [Export] public uint DaytimeStartInSeconds { get; private set; }
    [Export] public uint DaytimeEndInSeconds { get; private set; }
    [Export] public bool ShouldFreezeWhenEnded { get; private set; }
    public uint CurrentTime { get; private set; }
    private Timer _timer = null!;
    private bool _wasDaytime;

    public override void _Ready()
    {
        _timer = new Timer
        {
            WaitTime = TickIntervalInSeconds,
            Autostart = true,
            OneShot = false,
        };
        AddChild(_timer);
        CurrentTime = 0;
        _timer.Timeout += OnTimerTick;
        Validate();
    }

    private void Validate()
    {
        if (DaytimeEndInSeconds <= DaytimeStartInSeconds)
        {
            GD.PushError(this + " is misconfigured: DaytimeStartInSeconds must be greater than DaytimeEndInSeconds.");
        }
    }

    private void OnTimerTick()
    {
        CurrentTime += TimeSpeed * TickIntervalInSeconds;
        if (CurrentTime >= CycleDurationInSeconds)
        {
            if (ShouldFreezeWhenEnded)
            {
                CurrentTime = CycleDurationInSeconds;
            }
            else
            {
                CurrentTime -= CycleDurationInSeconds;
            }
            EmitSignalCycleEnded();
        }
        EmitSignalTimeChanged(CurrentTime);
    }

    public bool IsDaytime()
    {
        return CurrentTime >= DaytimeStartInSeconds && CurrentTime < DaytimeEndInSeconds;
    }

    public void Freeze()
    {
        SetProcess(false);
    }

    public void Unfreeze()
    {
        SetProcess(true);
    }
}