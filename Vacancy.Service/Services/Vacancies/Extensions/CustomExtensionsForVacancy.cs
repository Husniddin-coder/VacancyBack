namespace Vacancy.Service.Services.Vacancies.Extentions;

public static class CustomExtensionsForVacancy
{
    public static Dictionary<string, List<string>> ExtractPropertiesAndValues(this string? input)
    {
        var result = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        if (string.IsNullOrWhiteSpace(input))
        {
            return result;
        }

        var segments = input.Split(',');

        string? currentProperty = null;
        foreach (var segment in segments)
        {
            var colonIndex = segment.IndexOf(':');
            if (colonIndex != -1)
            {
                // New property found
                currentProperty = segment.Substring(0, colonIndex).Trim().ToLowerInvariant();
                var value = segment.Substring(colonIndex + 1).Trim();
                if (!result.ContainsKey(currentProperty))
                {
                    result[currentProperty] = new List<string>();
                }
                result[currentProperty].Add(value);
            }
            else if (currentProperty != null)
            {
                // Continue accumulating values for the current property
                result[currentProperty].Add(segment.Trim());
            }
        }

        return result;
    }

    public static (string Property, string OrderType) ExtractPropertyAndOrder(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return (string.Empty, string.Empty);
        }

        var colonIndex = input.IndexOf(':');
        if (colonIndex == -1)
        {
            return (input.Trim().ToLowerInvariant(), string.Empty);
        }

        var property = input.Substring(0, colonIndex).Trim().ToLowerInvariant();
        var orderType = input.Substring(colonIndex + 1).Trim().ToLowerInvariant();

        // Ensure order type is either "asc" or "desc"
        if (orderType != "asc" && orderType != "desc")
        {
            orderType = string.Empty;
        }

        return (property, orderType);
    }
}