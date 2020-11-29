using System;
namespace Books.MediatR
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public ErrorCode? ErrorCode { get; set; }

        public static Result Success()
        {
            return new Result { IsSuccess = true };
        }

        public static Result Failure(ErrorCode errorCode)
        {
            return new Result { IsSuccess = false, ErrorCode = errorCode };
        }

        public static Result<T> Success<T>(T body)
        {
            return new Result<T> { IsSuccess = true, Body = body };
        }

        public static Result<T> Failure<T>(ErrorCode errorCode)
        {
            return new Result<T> { IsSuccess = false, ErrorCode = errorCode };
        }

        protected Result()
        {
        }
    }

    public class Result<T> : Result
    {
        public T Body { get; set; }
    }

    public enum ErrorCode
    {
        NotFound,
        BadRequest,
        IDontLikeYou
    }
}
