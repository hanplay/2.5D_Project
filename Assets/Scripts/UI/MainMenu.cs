using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private UI_WoodenButton startButton;
    private UI_WoodenButton quitButton;
    private void Awake()
    {
        startButton = transform.Find("StartButton").GetComponent<UI_WoodenButton>();
        quitButton = transform.Find("QuitButton").GetComponent<UI_WoodenButton>();

        startButton.Click += StartButton_Click;
        quitButton.Click += QuitButton_Click;
    }

    private void QuitButton_Click()
    {
        Application.Quit();
    }

    private void StartButton_Click()
    {
        SceneManager.LoadScene("Scenes/BattleScene/BattleScene", LoadSceneMode.Single);
    }
}
