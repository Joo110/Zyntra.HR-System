namespace HRMS.Contracts.Pagination;
public static class PagedList
{
    public static (IEnumerable<T> Items, int Total) Paginate<T>(IQueryable<T> query, int pageNumber, int pageSize)
    {
        var total = query.Count();
        var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return (items, total);
    }
}
 