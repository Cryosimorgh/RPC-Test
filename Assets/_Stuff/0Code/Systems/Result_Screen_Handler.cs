using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code
{
    public class Result_Screen_Handler : MonoBehaviour
    {
        [FormerlySerializedAs("gO")]
        [SerializeField] GameObject next_Round_UI;
        [SerializeField] TextMeshProUGUI text;

        private void Awake() => next_Round_UI.SetActive(false);

        private void OnEnable() => Game_Manager.Winner += Show_Win_Screen;

        private void OnDisable() => Game_Manager.Winner -= Show_Win_Screen;

        private void Show_Win_Screen(Win_State state, int[] scores)
        {
            var score = "You: " + scores[0] + " - "  + "Computer: " + scores[1];

            next_Round_UI.SetActive(true);

            if (state == Win_State.p1 || state == Win_State.p2)
            {
                string pl = state == Win_State.p1 ? "Player 1" : "Player 2";
                score = "Congratulations " + pl + "!"  + "\n" + "You won!" + "\n" + score;
            }
            else
            {
                score = "Results: \n" + state + "\n" + score;
            }

            score = score.Replace('_', ' ');

            text.text = score;
        }
    }
}
