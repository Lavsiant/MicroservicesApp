using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Discount.Grpc.Configuration
{
    public class LoggerInterceptor : Interceptor
    {
       // private readonly ILogger<LoggerInterceptor> _logger;

        public LoggerInterceptor()
        {
            //_logger = logger;
        }

        public async override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
            TRequest request,
            ServerCallContext context,
            UnaryServerMethod<TRequest, TResponse> continuation)
        {
            LogCall(context);
            try
            {
                return await continuation(request, context);
            }
            catch (Exception e)
            {
                // _logger.LogError(e, $"An error occured when calling {context.Method}");
                throw new RpcException(Status.DefaultCancelled, e.Message);
            }

        }

        private void LogCall(ServerCallContext context)
        {
            var httpContext = context.GetHttpContext();
            //  _logger.LogDebug($"Starting call. Request: {httpContext.Request.Path}");
        }
    }
}
