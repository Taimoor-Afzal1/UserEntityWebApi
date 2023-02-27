namespace UserEntity.Dtos
{
    public class PaginationDto<T> where T : class
    {
        public List<T> Values { get; set; } = new List<T>();
        public int CurrentPage { get; set; }
        public double TotalPages { get; set; }
        public int TotalRecords { get; set; }
    }
}
