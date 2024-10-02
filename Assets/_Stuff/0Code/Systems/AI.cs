using TMPro;
using UnityEngine;

namespace Code.AI
{
    /// <summary>
    /// MonoBehaviour class that represents an AI player in the game.
    /// </summary>
    public class AI : MonoBehaviour
    {
        /// <summary>
        /// The current turn phase of the game.
        /// </summary>
        [SerializeField] private Turn_Phase turn;
        [SerializeField] TextMeshProUGUI text;

        /// <summary>
        /// Subscribes to the OnGameStateChanged event of the Game_Manager instance when the component is enabled.
        /// </summary>
        private void OnEnable() => Game_Manager.Instance.Phase.OnGameStateChanged += Random_RPC;

        /// <summary>
        /// Unsubscribes from the OnGameStateChanged event of the Game_Manager instance when the component is disabled.
        /// </summary>
        private void OnDisable() => Game_Manager.Instance.Phase.OnGameStateChanged -= Random_RPC;

        /// <summary>
        /// Randomly selects and provides a player choice when the current turn phase matches the AI's turn phase.
        /// </summary>
        /// <param name="turn">The current turn phase.</param>
        async void Random_RPC(Turn_Phase turn)
        {
            // If the current turn phase is not the same as the AI's turn phase, do nothing.
            if (turn == this.turn)
                await Async_RPC();
        }

        private async Awaitable Async_RPC()
        {
            text.text = "Enemy Choosing...";
            await Awaitable.WaitForSecondsAsync(.5f);
            

            // Randomly select and provide a choice.
            var choice = RPC.Provider.Provide();

            // Update the text and handle the choice.
            text.text = "Enemy Chose:\n" + choice;

            // Feeds the choice to the handler.
            RPC.Handler.Handle(choice);

            // Shift the phase with a slight delay.
            await Awaitable.WaitForSecondsAsync(.5f);
            Game_Manager.Instance.Next_Phase();

            text.text = "";
        }
    }
}
