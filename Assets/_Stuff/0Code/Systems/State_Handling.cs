/// <summary>
/// Manages the state of the game.
/// </summary>
/// <typeparam name="T">The type of the game state enum.</typeparam>
public class State_Handling<T> where T : System.Enum
{
    /// <summary>
    /// Event that is raised when the game state changes.
    /// </summary>
    public event System.Action<T> OnGameStateChanged;

    // The current state of the game
    private T _state;

    /// <summary>
    /// Gets or sets the current state of the game.
    /// </summary>
    public T State
    {
        get
        {
            return _state;
        }
        set
        {
            // Only update the state if it has changed
            if (!_state.Equals(value))
            {
                _state = value;
                // Raise the OnGameStateChanged event
                OnGameStateChanged?.Invoke(_state);
            }
        }
    }
}