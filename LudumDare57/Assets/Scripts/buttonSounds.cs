using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public AudioClip[] clickSounds;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void PlaySound()
    {
        if (clickSounds.Length > 0 && audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                int index = Random.Range(0, clickSounds.Length);
                audioSource.clip = clickSounds[index];
                audioSource.Play();
            }
        }
    }
}
