using System;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource audioSource;




    public string PlaySound(AudioSource source)
    {
        // Play the sound at the collider's position
        source.Play();


        return null;
    }

    public void StopSound(AudioSource source)
    {
        source.Stop();
    }
}
