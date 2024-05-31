namespace FlexCore.Models
{
    public class Pageable
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 10;
        public List<Sort>? Sort { get; set; }
    }
}
