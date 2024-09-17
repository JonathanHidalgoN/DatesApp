using System.Text.Json;

namespace API;

public static class HttpExtensions{

    /**
    * This method is used to add pagination headers to the response
    * @param response: HttpResponse
    * @param data: PagedList<T>
    */
    public static void addPaginationHeader<T>(this HttpResponse response, PagedList<T> data){
        var PaginationHeaders = new PaginationHeaders(data.currentPage, data.pageSize, data.totalCount, data.totalPages);
        var jsonOptions = new JsonSerializerOptions{
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        response.Headers.Append("Pagination", JsonSerializer.Serialize(PaginationHeaders, jsonOptions));
        response.Headers.Append("Access-Control-Expose-Headers", "Pagination");


    }
}