using Vacancy.Domain.Entities.Applications;
using Vacancy.Service.Configurations;

namespace Vacancy.Service.Helpers;

public class Response<T>
{
    public IEnumerable<T> Result { get; set; }

    public PaginationParams Pagination { get; set; }

    public IEnumerable<string>? Regions { get; set; }

    public IEnumerable<string>? Companies { get; set; }

    public long? Total { get; set; }

    public IList<AppStatus>? AllStatus { get; set; }
}
