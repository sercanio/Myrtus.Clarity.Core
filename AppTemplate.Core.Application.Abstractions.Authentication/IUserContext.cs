namespace AppTemplate.Core.Application.Abstractions.Authentication;

public interface IUserContext
{
  Guid UserId { get; }

  string IdentityId { get; set; }
}