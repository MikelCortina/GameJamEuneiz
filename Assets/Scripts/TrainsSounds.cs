using UnityEngine;

public class RunAudio : MonoBehaviour
{
    public AudioClip runSound;
    public AudioSource audioSource;


    void Start()
    {

        audioSource.clip = runSound;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        // Ahora sí podemos iniciar el sonido
        StartRun();
    }

    public void StartRun()
    {
        audioSource.volume = 0.25f; // 0 = silencio, 1 = volumen completo
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    public void StopRun()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
