using UnityEngine;

/// <summary>
/// Singleton to handle music and AFX playback.
/// </summary>
public class AudioManager : MonoBehaviour {

    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioSource[] m_audioShootSources;

    [SerializeField]
    private AudioClip[] m_shootSounds;

    [SerializeField]
    private int m_shootSoundID;

    [SerializeField]
    private bool m_muteSound;

    private int m_currentShootSoundCycleID = 0;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PreloadAudioSourceWithClip(m_audioShootSources, m_shootSounds[m_shootSoundID]);
    }

    /// <summary>
    /// Plays the shoot sound.
    /// </summary>
    public void PlayShootSound()
    {
        if (m_muteSound)
            return;

        //m_audioShootSources[m_currentShootSoundCycleID].Play();
        m_currentShootSoundCycleID++;

        m_currentShootSoundCycleID = m_currentShootSoundCycleID > m_audioShootSources.Length - 1 ? 0 : m_currentShootSoundCycleID++;
    }

    /// <summary>
    /// Preloads the AudioSources with AudioClips.
    /// </summary>
    /// <param name="audioSources">The AudioSources to load.</param>
    /// <param name="audioClip">The AudioClip the AudioSources should be loaded with.</param>
    private void PreloadAudioSourceWithClip(AudioSource[] audioSources, AudioClip audioClip)
    {
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.clip = audioClip;
        }
    }
}
