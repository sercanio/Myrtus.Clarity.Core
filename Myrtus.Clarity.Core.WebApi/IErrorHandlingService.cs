using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;

namespace Myrtus.Clarity.Core.WebAPI;

public interface IErrorHandlingService
{
    IActionResult HandleErrorResponse<T>(Result<T> result);
}
