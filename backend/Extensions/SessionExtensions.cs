// File: Extensions/SessionExtensions.cs
using Askii.backend.Model;
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
                SessionAdminUID = session.SessionAdminUID,
                SessionTopic = session.SessionTopic,
                CreatedAt = session.CreatedAt,
                SessionAttendeeUIDs = session.SessionAttendees?.Select(a => a?.UID).ToList(),
                SessionModeratorUIDs = session.SessionModerators?.Select(m => m?.UID).ToList()
            };
        }
    }
}


