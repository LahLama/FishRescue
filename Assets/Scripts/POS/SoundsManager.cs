using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct NamedAudioClip
{
    public string name;
    public AudioClip clip;

}

public class SoundsManager : MonoBehaviour
{
    float fadeDuration = 0.2f;
    Coroutine fadeCoroutine;
    public List<NamedAudioClip> audioClips = new List<NamedAudioClip>();

    private Dictionary<string, AudioClip> clipLookup;

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

    public void PlaySound(string clipName, bool loop, AudioSource audioSource)
    {
        AudioClip clip = GetClipByName(clipName);
        if (clip != null)
        {
            // Cancel any ongoing fade before starting a new one
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(FadeToNewClip(clip, loop, audioSource));
        }
        else
        {
            Debug.LogWarning($"Audio clip '{clipName}' not found in SoundsManager.");
        }
    }

    IEnumerator FadeToNewClip(AudioClip newClip, bool loop, AudioSource audioSource)
    {
        // Fade out the current clip if something is playing
        if (audioSource.isPlaying)
        {
            float startVolume = audioSource.volume;
            float elapsed = 0f;

            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                audioSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / fadeDuration);
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = 0f;
        }

        // Swap the clip and fade in
        audioSource.clip = newClip;
        audioSource.loop = loop;
        audioSource.Play();

        float fadeElapsed = 0f;
        while (fadeElapsed < fadeDuration)
        {
            fadeElapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, fadeElapsed / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f;
    }
}
