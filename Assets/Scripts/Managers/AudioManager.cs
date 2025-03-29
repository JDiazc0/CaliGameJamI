using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioMixer _masterMixer;
    [SerializeField] private AudioClip _defaultMenuMusic;
    private bool isMuted = false;

    private const string MusicVolumeKey = "Music";
    private const string SfxVolumeKey = "SFX";
    private const string MasterVolumeKey = "Master";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _sfxSource = transform.GetChild(0).GetComponent<AudioSource>();
        _musicSource = transform.GetChild(1).GetComponent<AudioSource>();

        LoadSoundPreferences();
        PlayMusic(_defaultMenuMusic);
    }


    // Música
    public void PlayMusic(AudioClip clip, bool loop = true)
    {
        if (_musicSource == null || clip == null) return;

        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.loop = loop;
        _musicSource.Play();
    }

    // SFX
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (_sfxSource != null && clip != null)
        {
            _sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void StopSFX() => _sfxSource?.Stop();

    // Volumen
    public void SetMasterVolume(float volumeDB)
    {
        _masterMixer.SetFloat(MasterVolumeKey, volumeDB);
        PlayerPrefs.SetFloat(MasterVolumeKey, volumeDB);
    }

    public void SetMusicVolume(float volumeDB)
    {
        _masterMixer.SetFloat(MusicVolumeKey, volumeDB);
        PlayerPrefs.SetFloat(MusicVolumeKey, volumeDB);
    }

    public void SetSFXVolume(float volumeDB)
    {
        _masterMixer.SetFloat(SfxVolumeKey, volumeDB);
        PlayerPrefs.SetFloat(SfxVolumeKey, volumeDB);
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        _masterMixer.SetFloat(MasterVolumeKey, isMuted ? -40f : 0f);
        PlayerPrefs.SetFloat(MasterVolumeKey, isMuted ? -40f : 0f);
    }

    private void LoadSoundPreferences()
    {
        if (PlayerPrefs.HasKey(MusicVolumeKey))
        {
            SetMusicVolume(PlayerPrefs.GetFloat(MusicVolumeKey));
        }
        if (PlayerPrefs.HasKey(SfxVolumeKey))
        {
            SetSFXVolume(PlayerPrefs.GetFloat(SfxVolumeKey));
        }
        if (PlayerPrefs.HasKey(MasterVolumeKey))
        {
            _masterMixer.SetFloat(MasterVolumeKey, PlayerPrefs.GetFloat(MasterVolumeKey));
        }
    }
}