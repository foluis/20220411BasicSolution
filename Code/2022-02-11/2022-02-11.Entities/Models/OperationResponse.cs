namespace _2022_02_11.Entities.Models
{
    public class OperationResponse<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}