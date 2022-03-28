using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public AudioSource audioSource;
    public void LoadGameScene(){
        SceneManager.LoadScene("MainGame");
        audioSource.Play();
    }
}
