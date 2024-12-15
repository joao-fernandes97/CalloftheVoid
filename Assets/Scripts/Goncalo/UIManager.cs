using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject areYouSurePanel;
    [SerializeField]
    private PlayerMovement playerMovement;

    private bool gamePaused = false;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !gamePaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            playerMovement.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.P) && gamePaused)
        {
            Resume();
        }
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        areYouSurePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement.enabled = true;
    }

    public void MainMenuButton()
    {
        areYouSurePanel.SetActive(true);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BackToPause()
    {
        areYouSurePanel.SetActive(false);
    }
}
