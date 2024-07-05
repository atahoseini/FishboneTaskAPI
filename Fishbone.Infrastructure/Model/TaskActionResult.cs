namespace Fishbone.Infrastructure.Model
{
    public class TaskActionResult<T>  
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int Total { get; set; } //=> like 1469
        public int Page { get; set; } // => 3
        public int PageCount
        {
            get
            {
                if (Total == 0) return 0;
                return Convert.ToInt32(Math.Ceiling(Total / (double)Size));
            }
        }
        public int Size { get; set; }
    }
}
