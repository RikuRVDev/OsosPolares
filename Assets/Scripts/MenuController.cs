using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance = null;

    public void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void GoToCredits() {
        SceneManager.LoadScene("Creditos");
    }

    public void GoToGameOver() {
        SceneManager.LoadScene("GameOver");
        Invoke("GoToMainMenu",5.0f);
    }

    public void GoToWin() {
        SceneManager.LoadScene("Win");
        Invoke("GoToMainMenu",5.0f);
    }

    private void GoToMainMenu() {
        SceneManager.LoadScene("Menu");
    }
}
