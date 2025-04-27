using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Audio Setup")]
    public AudioSource musicSource;

    [System.Serializable]
    public class MusicTrack
    {
        public string key;         // "ToSoulPhase_0", "Soul_3", etc.
        public AudioClip clip;
    }

    [Header("Music Tracks")]
    public List<MusicTrack> tracks;

    private Dictionary<string, AudioClip> musicDict;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Build quick lookup from key -> clip
        musicDict = new Dictionary<string, AudioClip>();
        foreach (var track in tracks)
        {
            if (!musicDict.ContainsKey(track.key) && track.clip != null)
                musicDict.Add(track.key, track.clip);
        }
    }

    public void Play(string key)
    {
        if (musicDict.TryGetValue(key, out AudioClip clip))
        {
            musicSource.clip = clip;
            musicSource.Play();
            Debug.Log("?? MusicManager: Playing " + key);
        }
        else
        {
            Debug.LogWarning("?? MusicManager: No track found for key '" + key + "'");
        }
    }

    public void Stop()
    {
        musicSource.Stop();
    }
}
