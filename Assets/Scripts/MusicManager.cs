using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] audioSources;

    private int currentTrackIndex = 0;

    void Start()
    {
        if (audioSources.Length > 0)
        {
            audioSources[currentTrackIndex].Play();
        }
    }

    void Update()
    {
        if (audioSources[currentTrackIndex].isPlaying)
            return;

        currentTrackIndex = (currentTrackIndex + 1) % audioSources.Length;
        audioSources[currentTrackIndex].Play();
    }
}