namespace ProjectAssignment.Models
{
    //Common Response
    public class CommonResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public CommonResponse(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static CommonResponse<T> Success(T data, string message = "Request was successful.")
        {
            return new CommonResponse<T>(true, message, data);
        }

        public static CommonResponse<T> Fail(string message)
        {
            return new CommonResponse<T>(false, message, default);
        }


    }
}
