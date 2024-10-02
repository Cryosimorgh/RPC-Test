using System.Linq;

namespace Code
{
    /// <summary>
    /// Manages the game state and transitions between different states.
    /// </summary>
    [UnityEngine.DefaultExecutionOrder(-999)]
    public class Game_Manager : Persistent_Singleton<Game_Manager>
    {
        /// <summary>
        /// The scores of the two players.
        /// </summary>
        private int[] scores = new int[2];

        /// <summary>
        /// Broadcasts the winner of the game.
        /// </summary>
        public static event System.Action<Win_State, int[]> Winner;

        /// <summary>
        /// The current turn of the game.
        /// </summary>
        public State_Handling<Turn_Phase> Phase { get; private set; }

        /// <summary>
        /// A circular iterator for the different phases of the game.
        /// </summary>
        CircularIterator<Turn_Phase> play_State;

        /// <summary>
        /// Initializes the game manager and sets up the circular array of states.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            Phase = new State_Handling<Turn_Phase>();
            play_State = new CircularIterator<Turn_Phase>(System.Enum.GetValues(typeof(Turn_Phase)).Cast<Turn_Phase>().ToList());

        }

        /// <summary>
        /// Subscribes to the game phase changed event when the game manager is enabled.
        /// </summary>
        private void OnEnable() => Phase.OnGameStateChanged += RPC.Handler.Determine;

        /// <summary>
        /// Unsubscribes from the game phase changed event when the game manager is disabled.
        /// </summary>
        private void OnDisable() => Phase.OnGameStateChanged -= RPC.Handler.Determine;

        /// <summary>
        /// Transitions to the next state in the circular array of states.
        /// </summary>
        [NaughtyAttributes.Button]
        public void Next_Phase() => Phase.State = play_State.Next();

        /// <summary>
        /// Announces the winner of the game.
        /// </summary>
        /// <param name="state">The state of the winner.</param>
        public void Announce_Winner(Win_State state)
        {
            // Update the scores based on the round winner.
            if (state == Win_State.Player1_Won)
            {
                scores[0] += 1;

            }
            else if (state == Win_State.Player2_Won)
            {
                scores[1] += 1;

            }

            // Check if the game is over.
            if (scores[0] >= 5 || scores[1] >= 5)
            {
                Win_State final_State = scores[0] > scores[1] ? Win_State.p1 : Win_State.p2;
                Winner?.Invoke(final_State, scores);
                scores = new int[2];
                return;
            }

            // Invoke the Winner event with the given state.
            Winner?.Invoke(state, scores);


        }
    }
}