using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
    }

    // activate game over screen
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
    }

    // game over option functions

    public void Restart()
    {
        // .buildIndex returns the index of the current level, in reference to Build Settings in Unity
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }

    public void MainMenu()
    {
        // .buildIndex returns the index of the current level, in reference to Build Settings in Unity
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        // .buildIndex returns the index of the current level, in reference to Build Settings in Unity
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false; //only to exit game in Unity Play Mode
    }
}
