using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GoToMenuScript : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    void Awake()
    {
        StartCoroutine(WaitForVideoToEnd());
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }

    private IEnumerator WaitForVideoToEnd()
    {
        // Espera a que el video comience a reproducirse si aún no lo ha hecho
        while (!videoPlayer.isPlaying)
        {
            yield return null;
        }

        Debug.Log("Video length: " + videoPlayer.length);
        yield return new WaitForSeconds((float)videoPlayer.length);
        SceneManager.LoadScene(1);
    }
}
