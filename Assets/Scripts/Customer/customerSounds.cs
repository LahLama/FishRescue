using System.Collections.Generic;
using UnityEngine;


public class CustomerSounds : MonoBehaviour
{
    public List<NamedAudioClip> audioClips = new List<NamedAudioClip>();

    private Dictionary<string, AudioClip> clipLookup;
    public AudioSource audioSource;

    private void Awake()
    {
        clipLookup = new Dictionary<string, AudioClip>();
        foreach (var namedClip in audioClips)
        {
            if (!string.IsNullOrEmpty(namedClip.name) && namedClip.clip != null)
            {
                clipLookup[namedClip.name] = namedClip.clip;
            }
        }
    }

    public AudioClip GetClipByName(string clipName)
    {
        if (clipLookup != null && clipLookup.TryGetValue(clipName, out var clip))
        {
            return clip;
        }

        return null;
    }
    public void PlaySound(string clipName, bool loop = false)
    {
        AudioClip clip = GetClipByName(clipName);
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Audio clip '{clipName}' not found in SoundsManager.");
        }
    }
}
