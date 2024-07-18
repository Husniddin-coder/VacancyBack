using Vacancy.Service.Configurations;
using Vacancy.Service.Helpers;

namespace Vacancy.Service.Extensions;

public static class CollectionExtentions
{
    public static Response<TEntity> ToPagedList<TEntity>
            (this IQueryable<TEntity>? source, Params @params)
    {
        var pagination = new PaginationParams();
        int Length = source.Count();
        int begin = @params.Page * @params.Size;
        int end = Math.Min(@params.Size * (@params.Page + 1), Length);
        double lastPage = Math.Max(Math.Ceiling((double)Length / (double)@params.Size), 1);

        if (@params.Page > lastPage)
        {
            source = null;
            pagination = new PaginationParams { LastPage = lastPage };
        }
        else
        {
            source = source.Skip(begin).Take(end - begin);
            pagination = new PaginationParams
            {
                Page = @params.Page,
                Size = @params.Size,
                Length = Length,
                LastPage = lastPage,
                StartIndex = begin,
                EndIndex = end
            };
        }

        return new Response<TEntity>() { Result = source, Pagination = pagination };
    }

    public static Response<TEntity> ToPagedList<TEntity>(this IEnumerable<TEntity>? source, Params @params)
    {
        var pagination = new PaginationParams();
        int Length = source.Count();
        int begin = @params.Page * @params.Size;
        int end = Math.Min(@params.Size * (@params.Page + 1), Length);
        double lastPage = Math.Max(Math.Ceiling((double)Length / (double)@params.Size), 1);

        if (@params.Page > lastPage)
        {
            source = null;
            pagination = new PaginationParams { LastPage = lastPage };
        }
        else
        {
            source = source.Skip(begin).Take(end - begin);
            pagination = new PaginationParams
            {
                Page = @params.Page,
                Size = @params.Size,
                Length = Length,
                LastPage = lastPage,
                StartIndex = begin,
                EndIndex = end
            };
        }
        return new Response<TEntity>() { Result = source, Pagination = pagination };
    }
}
