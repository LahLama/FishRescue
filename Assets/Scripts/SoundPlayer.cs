using UnityEngine;

public class SoundPlayer : MonoBehaviour, IInteractable
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioClip = audioSource.clip;
    }
    public string Interact(Collider col)
    {
        PlaySound();
        return null;
    }

    public void PlaySound()
    {
        
    }
}
