namespace KryptoCalc.Shared;

public class PaginatedParameter
{
    public string Url { get; }
    public string Result { get; }
    public int Page { get; }
    public int ViewCount { get; }
    public int ViewRange { get; }
    public int Total { get; }
    public int FirstPage { get; }
    public int BeginPage { get; }
    public int EndPage { get; }
    public int LastPage { get; }
    public bool IsBeginPage { get; }
    public bool IsEndPage { get; }
    public bool IsFirstPage { get; }
    public bool IsLastPage { get; }
    public bool IsPage { get; }

    public PaginatedParameter(int page, int count, int total, int viewRange, string url)
    {
        if (page < 1) throw new ArgumentOutOfRangeException();
        if (count < 0) throw new ArgumentOutOfRangeException();
        if (total < 0) throw new ArgumentOutOfRangeException();
        Url = url;
        Page = page;
        ViewCount = count;
        Total = total;
        ViewRange = viewRange;
        FirstPage = 1;

        var lastPage = count > 0 ? (total + count - 1) / count : 0;
        var beginPage = lastPage <= viewRange ? 1 : Math.Max(page - (viewRange - 2), 1);
        var endPage = lastPage <= viewRange ? lastPage : beginPage + viewRange - 1;
        if (endPage > lastPage)
        {
            endPage = lastPage;
            beginPage = endPage - viewRange + 1;
        }
        IsBeginPage = page > 1;
        IsEndPage = page < lastPage;
        IsFirstPage = beginPage > 1;
        IsLastPage = lastPage <= viewRange ? false : page != lastPage;
        IsPage = beginPage != endPage && total != 0;
        Result = IsPage ? $"全{total}件({(page - 1) * count + 1}~{Math.Min(page * count, total)}件)" : $"全{total}件";
        BeginPage = beginPage;
        EndPage = endPage;
        LastPage = lastPage;
    }
}
