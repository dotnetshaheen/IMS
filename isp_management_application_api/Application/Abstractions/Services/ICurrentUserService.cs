namespace Application.Abstractions.Services;

public interface ICurrentUserService : IScopedService
{
    public int UserId { get; }
    //public int RoleId { get; }
}
