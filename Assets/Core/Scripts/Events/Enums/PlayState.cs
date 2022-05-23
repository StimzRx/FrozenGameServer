namespace Core.Scripts.Events.Enums
{
    /// <summary>
    /// The PlayState of the current game instance
    /// </summary>
    public enum PlayState
    {
        Inactive = -1,      // Main Menu and other such non-connected states
        Unknown = 0,        // Not Used
        Lobby = 5,          // Connected to a server's Lobby
        Connecting = 7,     // Joining a server
        Joined = 10,        // Fully connected to a server
    }
}
