using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace Infrastructure.Midlewares
{
    internal class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            object? body = null;
            var request = context.Request;
            try
            {
                if(request.ContentType?.Contains("application/json") == true)
                {
                    request.EnableBuffering();
                    var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                    await request.Body.ReadAsync(buffer, 0, buffer.Length);

                    var requestContent = Encoding.UTF8.GetString(buffer);
                    body = JsonConvert.DeserializeObject<IDictionary<string, object>>(requestContent);
                    request.Body.Position = 0;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                if (body != null)
                    Log.Error(ex, "Unhandled Exception: {Path} {NewLine} Body: {@Body}", context.Request.Path, Environment.NewLine, body);
                else
                {
                    var query = context.Request.Query;
                    var result = new Dictionary<string, object>();

                    foreach (var key in query.Keys)
                    {
                        var value = query[key];
                        result[key] = value.Count > 1 ? value.ToArray() : value.ToString();
                    }

                    Log.Error(ex, "Unhandled Exception: {Path} {NewLine} Query: {@Query}", context.Request.Path, Environment.NewLine, result);
                }
                throw;
            }
        }
    }
}
