using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Exceptions
{
    public sealed class ApiException : Exception
    {
        public ApiException()
        {
            Source = "ApiService";
        }
        public ApiException(string message) : base($"Error while calling network: {message}")
        {
            Source = "ApiService";
        }
        public ApiException(string message, Exception exception) : base($"Error while calling network: {message}", exception)
        {
            Source = "ApiService";
        }
    }
}
