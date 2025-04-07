using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour 
{
    public void Retry()
    {
        Time.timeScale = 1f; // Unpause before reloading
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
