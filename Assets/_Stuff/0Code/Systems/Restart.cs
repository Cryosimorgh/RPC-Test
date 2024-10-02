using UnityEngine;

namespace Code
{
    public class Restart : MonoBehaviour
    {
        [SerializeField] UnityEngine.UI.Button btn;
        [SerializeField] GameObject gO;

        private void Awake() => btn.onClick.AddListener(() => 
        { 
            Game_Manager.Instance.Next_Phase();
            gO.SetActive(false);
        });
    }
}
