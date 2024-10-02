using UnityEngine;

namespace Code
{
    /// <summary>
    /// This class represents a button in the game. It handles the button's behavior based on the game's turn phase.
    /// </summary>
    public class Button : MonoBehaviour
    {
        #region Editor Only
#if UNITY_EDITOR
        [NaughtyAttributes.Button]
        private void UpdateGUI()
        {
            transform.name = choice.ToString();
            GetComponentInChildren<TMPro.TextMeshProUGUI>().text = choice.ToString();
        }
#endif
        #endregion
        /// <summary>
        /// The turn phase that this button is associated with.
        /// </summary>
        [SerializeField] Turn_Phase turn;

        /// <summary>
        /// The choice that this button makes when clicked.
        /// </summary>
        [SerializeField] RPC_Choice choice;

        UnityEngine.UI.Button btn;

        /// <summary>
        /// Initializes the button and sets up its click event listener.
        /// </summary>
        private void Awake()
        {
            btn = GetComponent<UnityEngine.UI.Button>();

            // Add a listener to the button's click event that calls the Handle method of the RPC.Handler class with the choice parameter.
            btn.onClick.AddListener(() => 
                { 
                    RPC.Handler.Handle(choice);
                    Game_Manager.Instance.Next_Phase();
                });
        }

        /// <summary>
        /// Subscribe to the game phase changed event when the button is enabled.
        /// </summary>
        private void OnEnable() => Game_Manager.Instance.Phase.OnGameStateChanged += Chech_Phase;

        /// <summary>
        /// Unsubscribe from the game phase changed event when the button is disabled.
        /// </summary>
        private void OnDisable() => Game_Manager.Instance.Phase.OnGameStateChanged -= Chech_Phase;

        /// <summary>
        /// Checks if the current game phase matches the turn phase associated with this button. If it does, enables the button, otherwise disables it.
        /// </summary>
        /// <param name="obj">The current game phase.</param>
        private void Chech_Phase(Turn_Phase obj) => btn.interactable = obj == turn;
    }
}
