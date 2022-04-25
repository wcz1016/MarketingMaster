using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public AudioSource audioSource;

    public void LoadGameScene(){
        SceneManager.LoadScene("MainGame");
        audioSource.Play();
    }

    public void EndGame()
    {
        Application.Quit();
        audioSource.Play();
    }
}
