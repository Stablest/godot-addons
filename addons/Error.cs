using System.Diagnostics;

namespace Addons.addons;

public static class Error
{
    public static void Assert(bool condition, string message, object reference)
    {
        Debug.Assert(condition, $"[{reference}] - " + message);
    }

    public static void AssertNotNull(object value, string varName, object reference)
    {
        Debug.Assert(value != null, $"[{reference}] - {varName} can't be null.");
    }
}