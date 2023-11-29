namespace Application.Models.Request;

public class PaginatedRequest
{
    private const int MAX_PAGE_SIZE = 50;
    public int Page { get; set; } = 1;

    private int _pageSize = 20;
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MAX_PAGE_SIZE ? MAX_PAGE_SIZE : value;
    }

    protected int Skip => Page * PageSize - PageSize;
}