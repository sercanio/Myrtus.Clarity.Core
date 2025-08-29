using MediatR;
using Ardalis.Result;

namespace AppTemplate.Core.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
