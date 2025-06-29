// File: Extensions/SessionExtensions.cs
using Askii.backend.Model;
using Askii.backend.Model.Enums;
using Askii.backend.DTOs.Session;

namespace Askii.backend.Extensions.SessionExtensions
{
    public static class SessionExtensions
    {
        public static SessionDTO ToDTO(this Session session)
        {
            return new SessionDTO
            {
                SessionID = session.SessionID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                Users = session.SessionParticipants?
                    .Select(us => new SessionUserDTO
                    {
                        UID = us.User.UID,
                        UserName = us.User.UserName,
                        Role = us.Role
                    }).ToList() ?? new List<SessionUserDTO>()
            };
        }
    }
}


