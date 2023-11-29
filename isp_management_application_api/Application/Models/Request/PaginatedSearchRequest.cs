namespace Application.Models.Request;

public class PaginatedSearchRequest<TStatus, TField> : PaginatedRequest
{    
    private string _keyword = string.Empty;
    public string Keyword
    {
        get => string.IsNullOrEmpty(_keyword) ? _keyword : _keyword.Trim().ToLower();
        set
        {
            _keyword = value;            
        }
    }
    public TStatus? Status { get; set; }
    public TField? Field { get; set; }

}
