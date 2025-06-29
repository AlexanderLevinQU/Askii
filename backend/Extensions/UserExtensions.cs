using Askii.backend.DTOs.User;
using Askii.backend.Extensions.SessionExtensions;
using Askii.backend.Model;

public static class UserExtensions
{
    public static UserDetailsDTO ToDetailsDTO(this User user)
    {
        return new UserDetailsDTO
        {
            UID = user.UID,
            UserName = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            LastLoginAt = user.LastLoginAt,
            DateOfBirth = user.DateOfBirth,
            // Convert UserSessions to UserSessionDTOs
            Sessions = user.UserSessions?
                .Select(us => new UserSessionDTO
                {
                    Session = us.Session.ToDTO(),
                    Role = us.Role
                }).ToList() ?? new List<UserSessionDTO>()
        };
    }
}