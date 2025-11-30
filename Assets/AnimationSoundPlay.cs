using UnityEngine;

public class AnimationSoundPlay : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundLatido1()
    {
        audioSource.PlayOneShot(audioClip);
    }
    public void StopSoundLatido1()
    {
        audioSource.Stop();
    }
}
