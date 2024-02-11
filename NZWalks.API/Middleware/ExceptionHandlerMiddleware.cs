﻿using System.Net;

namespace NZWalks.API.Middleware
{
	public class ExceptionHandlerMiddleware
	{
		private readonly ILogger<ExceptionHandlerMiddleware> logger;
		private readonly RequestDelegate next;

		public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger,
            RequestDelegate next)
        {
			this.logger = logger;
			this.next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await next(httpContext);
			}
			catch(Exception ex)
			{
				// Log the exception

				var errorId = Guid.NewGuid();
				logger.LogError(ex,$"{errorId} : {ex.Message}");

				// Return Custom error response

				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				httpContext.Response.ContentType = "application/json";

				var error = new
				{
					Id = errorId,
					ErrorMessage = "Something went wrong!!!"
				};
				await httpContext.Response.WriteAsJsonAsync(error);
			}
		}
    }
}
