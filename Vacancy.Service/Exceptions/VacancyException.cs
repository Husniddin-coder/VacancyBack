namespace Vacancy.Service.Exceptions;

public class VacancyException : Exception
{
    public int StatusCode { get; set; }

    public VacancyException(int Code, string Message) : base(Message)
    {
        StatusCode = Code;
    }
}
