using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance = null;
    private AudioManagerMenu audioManagerMenu;

    public void Awake() {
        audioManagerMenu = GetComponent<AudioManagerMenu>();
    }

    public void Start() {
        audioManagerMenu.StartMenuMusic();
    }

    public void PlayGame() {
        Debug.Log("play!");
        audioManagerMenu.StopAllCoroutines();
        audioManagerMenu.StopMenuMusic();
        SceneManager.LoadScene("Game");
    }

     public void QuitGame(){
        Application.Quit();
        Debug.Log("Quit");
    }

    public void GoToCredits() {
        SceneManager.LoadScene("Creditos");
    }
}
