namespace Vacancy.Service.Configurations;

public class PaginationParams
{
    public int Length { get; set; }
    public int Size { get; set; } = 10;
    public int Page { get; set; }
    public double LastPage { get; set; }
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
}
