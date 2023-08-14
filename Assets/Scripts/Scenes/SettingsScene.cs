using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsScene : MonoBehaviour
{
    [SerializeField]
    Button ResetProgressButton, backButtonButton;

    GameManager gameManager;

    void Start()
    {
        if (ResetProgressButton == null || backButtonButton == null)
            throw new MissingReferenceException("Required UI Buttons not available");

        gameManager = GameManager.Instance;
        if (gameManager == null)
            throw new MissingReferenceException("GameManager isn't available");

        ResetProgressButton.onClick.AddListener(ResetProgressButtonAction);
        backButtonButton.onClick.AddListener(backButtonButtonAction);
    }

    void ResetProgressButtonAction()
    {
        PlayerPrefs.DeleteAll();
        Image image = ResetProgressButton.GetComponent<Image>();
        if(image != null)
        {
            image.color = Color.green;
            ResetProgressButton.enabled = false;
        }
    }

    void backButtonButtonAction()
    {
        LoadScene(gameManager.findScene("Initial"));
    }

    void LoadScene(SceneBlock scene)
    {
        if (scene != null)
            SceneManager.LoadScene(scene.BuildIndex);
    }
}

