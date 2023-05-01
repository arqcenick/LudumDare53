using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryScript : MonoBehaviour
{
    public void PlayGame()
    {
        Invoke("LoadScene", 0.5f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("UI");
    }
}
