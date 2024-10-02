using System;
using System.Collections.Generic;

namespace Code.RPC
{
    /// <summary>
    /// Static class that handles the logic for the Rock-Paper-Scissors game.
    /// </summary>
    public static class Handler
    {
        /// <summary>
        /// Queue of player choices.
        /// </summary>
        static Queue<RPC_Choice> choices = new();

        /// <summary>
        /// Determines the winner of the game based on the current turn phase.
        /// </summary>
        /// <param name="turn">The current turn phase.</param>
        public static void Determine(Turn_Phase turn)
        {
            // If the current turn phase is not the result phase, do nothing.
            if (turn != Turn_Phase.Result)
                return;

            // Determine the winner based on the current choices and broadcast it to Game Manager.
            Game_Manager.Instance.Announce_Winner(Logic());

            // Clear the choices queue.
            choices = new();
        }

        /// <summary>
        /// Extracts two choices from the choices queue.
        /// </summary>
        /// <param name="choices">The queue of choices.</param>
        /// <returns>A tuple containing the first and second choices.</returns>
        private static (RPC_Choice, RPC_Choice) Extract(Queue<RPC_Choice> choices) => (choices.Dequeue(), choices.Dequeue());

        /// <summary>
        /// Adds a player choice to the choices queue.
        /// </summary>
        /// <param name="choice">The player choice to add.</param>
        public static void Handle(RPC_Choice choice) => choices.Enqueue(choice);

        /// <summary>
        /// Determines the winner of the game based on the current choices.
        /// </summary>
        /// <returns>The winner of the game.</returns>
        /// <exception cref="InvalidOperationException">If an invalid choice is encountered.</exception>
        public static Win_State Logic()
        {
            // Extract the first two choices from the queue.
            (var c1, var c2) = Extract(choices);

            // If the choices are the same, it's a Draw.
            if (c1 == c2) return Win_State.Draw;

            // Determine the winner based on the choices.
            return c1 switch
            {
                RPC_Choice.Rock => (c2 == RPC_Choice.Scissor) ? Win_State.Player1_Won : Win_State.Player2_Won,
                RPC_Choice.Paper => (c2 == RPC_Choice.Rock) ? Win_State.Player1_Won : Win_State.Player2_Won,
                RPC_Choice.Scissor => (c2 == RPC_Choice.Paper) ? Win_State.Player1_Won : Win_State.Player2_Won,
                _ => throw new InvalidOperationException("Invalid choice"),
            };
        }
    }
}
