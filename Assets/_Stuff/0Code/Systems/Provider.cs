using System.Linq;
using UnityEngine;

namespace Code.RPC
{
    /// <summary>
    /// Static class for making random player choice in RPC game.
    /// </summary>
    public static class Provider
    {
        /// <summary>
        /// List of all possible player choices.
        /// </summary>
        private static readonly System.Collections.Generic.List<RPC_Choice> rPC_Choices = System.Enum.GetValues(typeof(RPC_Choice)).Cast<RPC_Choice>().ToList();

        /// <summary>
        /// Returns a random player choice from the list of choices. If a choice is provided, it will be returned instead.
        /// </summary>
        /// <param name="choice">Optional player choice to return.</param>
        /// <returns>A random player choice from the list of choices.</returns>
        public static RPC_Choice Provide(RPC_Choice? choice = null)
        {
            // If a choice is provided, return it.
            if (choice.HasValue) 
                return choice.Value;
        
            // Otherwise, return a random choice from the list.
            return rPC_Choices[Random.Range(0, rPC_Choices.Count)];
        }
    }
}
