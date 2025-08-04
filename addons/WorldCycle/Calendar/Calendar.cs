using Godot;

namespace Addons.addons.WorldCycle.Calendar;

[GlobalClass]
public partial class Calendar : Node
{
    [Signal]
    public delegate void DayPassedEventHandler(uint currentDay);
    
    [ExportGroup("Year parameters")]
    [Export] public uint MonthsQuantity { get; private set; }
    [Export] public uint WeeksQuantity { get; private set; }
    [Export] public uint DaysQuantity { get; private set; }
    [ExportGroup("Current value")]
    [Export] public uint CurrentYear { get; private set; }
    [Export] public uint CurrentDayOfYear { get; private set; }
    private uint _daysOfYear; 
    
    private void Validate()
    {
        if (MonthsQuantity <= 0)
        {
            GD.PushError(this + " is misconfigured: MonthsQuantity must be greater than 0.");
        }

        if (WeeksQuantity <= 0)
        {
            GD.PushError(this + " is misconfigured: WeeksQuantity must be greater than 0.");
        }

        if (DaysQuantity <= 0)
        {
            GD.PushError(this + " is misconfigured: DaysQuantity must be greater than 0.");
        }
    }

    public override void _Ready()
    {
        Validate();
        _daysOfYear = DaysQuantity * WeeksQuantity * MonthsQuantity;
    }
    
    public uint GetDaysOfYear()
    {
        return _daysOfYear;
    }

    /// <summary>
    /// Pass the day and verify if it's needed to change the year.
    /// </summary>
    public void PassDay()
    {
        var nextDay = CurrentDayOfYear + 1;
        if (nextDay >= _daysOfYear)
        {
            CurrentDayOfYear = 0;
            CurrentYear++;
        }
        else
        {
            CurrentDayOfYear = nextDay;
        }
        EmitSignalDayPassed(CurrentDayOfYear);
    }

    /// <summary>
    /// Returns the current month index (0-based).
    /// </summary>
    public uint GetCurrentMonth()
    {
        return CurrentDayOfYear / (DaysQuantity * WeeksQuantity);
    }

    /// <summary>
    /// Returns the current day of the month (0-based).
    /// </summary>
    public uint GetCurrentDayOfMonth()
    {
        return CurrentDayOfYear / (DaysQuantity * WeeksQuantity);
    }

    /// <summary>
    /// Returns the current week of the month (0-based).
    /// </summary>
    public uint GetCurrentWeekOfMonth()
    {
        return (CurrentDayOfYear % (DaysQuantity * WeeksQuantity)) / WeeksQuantity;
    }
}