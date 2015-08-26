using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource[] m_audioSources;
    //private Dictionary<string, AudioSource> m_audioSources;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public AudioSource GetSoundByID(int soundID)
    {
        AudioSource audioSource;

        audioSource = m_audioSources[soundID];
        //m_audioSources.TryGetValue(soundID, out audioSource);

        return audioSource;
    }

}
