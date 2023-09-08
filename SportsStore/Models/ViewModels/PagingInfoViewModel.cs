namespace SportsStore.Models.ViewModels;

public class PagingInfoViewModel
{
    private int _startPage, _endPage;
    public int TotalItems { get; set; }
    public int ItemsPerPage { get; set; }
    public int CurrentPage { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }

    public int TotalPages =>
    (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);

    public void PageSettings()
    {
        _startPage = CurrentPage - 4;
        _endPage = CurrentPage + 5;
        if (_startPage < 1)
        {
            _endPage = _endPage - (_startPage - 1);
            _startPage = 1;
        }
        if (_endPage > TotalPages)
        {
            _endPage = TotalPages;
            if (_endPage > 10)
            {
                _startPage = _endPage - 9;
            }
        }
        StartPage = _startPage;
        EndPage = _endPage;
    }

}