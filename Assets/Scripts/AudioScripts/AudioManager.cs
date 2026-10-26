using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Audio Mixer")]
    public AudioMixer mixer;
    [Header("Music")]
    public AudioSource musicSource;
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip gameOverMusic;
    [Header("SFX")]
    public AudioSource SfxSource;
    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip hitSound;
    public AudioClip jumpSound;
    public AudioClip playerHit;
    public AudioClip hurtEnemy;
    public AudioClip deathEnemy;
    public AudioClip hitEnemy;
    public AudioClip slideSound;
    public AudioClip runSound;
    [Header("UI")]
    public AudioSource uiSource;
    public AudioClip buttonSound;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (mixer == null)
        {
            Debug.LogError("AudioMixer is not assigned.");
            return;
        }
        musicSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];
        SfxSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        uiSource.outputAudioMixerGroup = mixer.FindMatchingGroups("UI")[0];
    }
    private AudioMixerGroup GetAudioGroup(string groupName)
    {
        AudioMixerGroup[] groups = mixer.FindMatchingGroups(groupName);

        if (groups.Length > 0)
        {
            return groups[0];
        }

        Debug.LogError("AudioMixer group not found: " + groupName);
        return null;
    }
    private void Start()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "MainMenu")
            PlayMusic(mainMenuMusic);
        else if (scene == "GameOver")
            PlayMusic(gameOverMusic);
        else
            PlayMusic(gameplayMusic);
    }
    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }
    public void SetMusicVolume(float value) =>
        mixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    public void SetSfxVolume(float value) =>
        mixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    public void SetUiVolume(float value) =>
        mixer.SetFloat("UIVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    public void SetMasterVolume(float value) =>
        mixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20f);
    public void PlayHurt() => SfxSource.PlayOneShot(hurtSound);
    public void PlayDeath() => SfxSource.PlayOneShot(deathSound);
    public void PlayHit() => SfxSource.PlayOneShot(hitSound);
    public void PlayJump() => SfxSource.PlayOneShot(jumpSound);
    public void PlayPlayerHit() => SfxSource.PlayOneShot(playerHit);
    public void PlayHurtEnemy() => SfxSource.PlayOneShot(hurtEnemy);
    public void PlayDeathEnemy() => SfxSource.PlayOneShot(deathEnemy);
    public void PlayHitEnemy() => SfxSource.PlayOneShot(hitEnemy);
    public void PlaySlide() => SfxSource.PlayOneShot(slideSound);
    public void PlayButton()
    {
        if (uiSource != null && buttonSound != null)
        {
            uiSource.PlayOneShot(buttonSound);
        }
    }
    public void PlayGameOver() => PlayMusic(gameOverMusic);
}
