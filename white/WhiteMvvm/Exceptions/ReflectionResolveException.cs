using System;
using System.Collections.Generic;
using System.Text;

namespace WhiteMvvm.Exceptions
{
    public sealed class ReflectionResolveException : Exception
    {
        public ReflectionResolveException()
        {
            Source = "ApiService";
        }
        public ReflectionResolveException(string message) : base($"Error while resolve: {message}")
        {
            Source = "ApiService";
        }
        public ReflectionResolveException(string message, Exception exception) : base($"Error while resolve: {message}", exception)
        {
            Source = "ApiService";
        }
    }
}
