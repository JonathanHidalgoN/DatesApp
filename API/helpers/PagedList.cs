using Microsoft.EntityFrameworkCore;

namespace API;

public class PagedList<T> : List<T>{
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize){
        currentPage = pageNumber;
        totalPages = (int)Math.Ceiling(count / (double)pageSize);
        totalCount = count;
        this.pageSize = pageSize;
        AddRange(items); 
    }

    public int currentPage { get; set; }
    public int totalPages { get; set; }
    public int pageSize { get; set; }
    public int totalCount { get; set; }

    public static async Task<PagedList<T>> createAsync(IQueryable<T> source, int pageNumber, int pageSize){
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1 )* pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
    
}