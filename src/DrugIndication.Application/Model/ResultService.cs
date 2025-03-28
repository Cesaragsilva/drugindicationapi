namespace DrugIndication.Application.Model
{
    public class ResultService
    {
        private const string ErrorMessage = "Error";
        private const string SuccessMessage = "Success";
        public bool IsSuccess { get; set; }
        public string? Message { get; private set; }

        public static ResultService Fail(string message = ErrorMessage) => new() { IsSuccess = false, Message = message };
        public static ResultService<T> Fail<T>(string message = ErrorMessage) => new() { IsSuccess = false, Message = message };
        public static ResultService Ok(string message = SuccessMessage) => new() { IsSuccess = true, Message = message };
        public static ResultService<T> Ok<T>(T data, string message = SuccessMessage) => new() { IsSuccess = true, Message = message, Data = data };
    }

    public class ResultService<T> : ResultService
    {
        public T? Data { get; set; }
    }
}
