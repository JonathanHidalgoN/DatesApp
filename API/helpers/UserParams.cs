namespace API;

public class UserParams{

    private const int Max = 50;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > Max) ? Max : value;
    } 

    public string? gender { get; set; }

    public string? currentUsername { get; set; }

    public int MinAge { get; set; } = 18;

    public int MaxAge { get; set; } = 150;

    public string? orderBy { get; set; } = "lastActive";
}