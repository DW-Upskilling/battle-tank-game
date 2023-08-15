using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    [SerializeField]
    Button startGameButton, settingsButton;

    GameManager gameManager;

    void Start()
    {
        if (startGameButton == null || settingsButton == null)
            throw new MissingReferenceException("Required UI Buttons not available");

        gameManager = GameManager.Instance;
        if(gameManager == null)
            throw new MissingReferenceException("GameManager isn't available");

        startGameButton.onClick.AddListener(startGameButtonAction);
        settingsButton.onClick.AddListener(settingsButtonAction);
    }

    void startGameButtonAction() {
        LoadScene(gameManager.findScene("Game"));
    }

    void settingsButtonAction() {
        LoadScene(gameManager.findScene("Settings"));
    }

    void LoadScene(SceneBlock scene)
    {
        if (scene != null)
            SceneManager.LoadScene(scene.BuildIndex);
    }
}
