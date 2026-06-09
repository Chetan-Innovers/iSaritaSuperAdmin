using System.Collections.Concurrent;

namespace Identity.API.Services
{
    public interface ITokenBlacklistService
    {
        void BlacklistToken(string token);
        bool IsTokenBlacklisted(string token);
    }

    public class TokenBlacklistService : ITokenBlacklistService
    {
        // Thread-safe dictionary to hold blacklisted tokens.
        // We use string for the token and DateTime for when it was added.
        private readonly ConcurrentDictionary<string, DateTime> _blacklistedTokens = new();

        public void BlacklistToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return;

            // Remove 'Bearer ' prefix if present
            var cleanToken = token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) 
                ? token.Substring(7).Trim() 
                : token.Trim();

            // Add the token to the blacklist with the current UTC time
            _blacklistedTokens.TryAdd(cleanToken, DateTime.UtcNow);
        }

        public bool IsTokenBlacklisted(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return false;

            var cleanToken = token.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) 
                ? token.Substring(7).Trim() 
                : token.Trim();

            return _blacklistedTokens.ContainsKey(cleanToken);
        }
    }
}
