namespace _2022_02_11.Entities.Models
{
    public class CollectionResponse<T> : BaseResponse
    {
        public int? PageNumber { get; set; }
        public int? PagesCount { get; set; }
        public int? PageSize { get; set; }
        public IEnumerable<T> Records { get; set; }
    }
}