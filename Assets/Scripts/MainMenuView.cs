using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonYoutube;

    public Button ButtonPlay => buttonPlay;
    public Button ButtonExit => buttonExit;
    public Button ButtonYoutube => buttonYoutube;

    public void DisableAllButtons()
    {
        buttonPlay.enabled = false;
        buttonExit.enabled = false;
        buttonYoutube.enabled = false;
    }
}
