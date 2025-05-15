﻿using Microsoft.AspNetCore.Mvc;

namespace WebApiErrorHandling.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public ProblemDetails? Error { get; set; }

        public static ApiResponse<T> Ok(T data)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Error = null
            };
        }

        public static ApiResponse<T> Fail(ProblemDetails error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Data = default,
                Error = error
            };
        }
    }
}
