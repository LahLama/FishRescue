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


}
