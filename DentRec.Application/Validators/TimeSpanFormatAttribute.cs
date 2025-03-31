using System.ComponentModel.DataAnnotations;
using System.Globalization;

public class TimeSpanFormatAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value is null) return true; // Allow null values (optional fields)

        if (value is string timeString)
        {
            return TimeSpan.TryParseExact(timeString, @"hh\:mm\:ss", CultureInfo.InvariantCulture, out _);
        }

        return false;
    }

    public override string FormatErrorMessage(string name)
    {
        return $"{name} must be in the format hh:mm:ss.";
    }
}