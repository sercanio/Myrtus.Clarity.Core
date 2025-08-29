using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Core.WebApi;

public interface IErrorHandlingService
{
    IActionResult HandleErrorResponse<T>(Result<T> result);
}
