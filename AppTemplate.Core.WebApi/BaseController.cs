using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppTemplate.Core.WebApi;

  [Route("api/[controller]")]
  [ApiController]
  public class BaseController : ControllerBase
  {
      protected readonly ISender _sender;
      protected readonly IErrorHandlingService _errorHandlingService;

      public BaseController(ISender sender, IErrorHandlingService errorHandlingService)
      {
          _sender = sender;
          _errorHandlingService = errorHandlingService;
      }
  }
