using Project.Application.Enums;

namespace Project.Application.Results
{
    public class Result
    {
        public OperationStatus Status { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public bool IsSuccess => Status == OperationStatus.Success;

        public static Result Succeed(string? message = null)
            => new() { Status = OperationStatus.Success, Message = message };

        public static Result Failure(OperationStatus status, string message)
            => new() { Status = status, Message = message };

        public static Result Failure(OperationStatus status, List<string> errors)
            => new() { Status = status, Errors = errors, Message = string.Join("; ", errors) };
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Succeed(T data, string? message = null)
            => new() { Status = OperationStatus.Success, Data = data, Message = message };

        public new static Result<T> Failure(OperationStatus status, string message)
            => new() { Status = status, Message = message };

        public new static Result<T> Failure(OperationStatus status, List<string> errors)
            => new() { Status = status, Errors = errors, Message = string.Join("; ", errors) };
    }
}
