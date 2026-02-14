namespace Shefaa.Contracts.Users;

public record AuthenticationResponse(
    UserResponse User,
    string Token
);
