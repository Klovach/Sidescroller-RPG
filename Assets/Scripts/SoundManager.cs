using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    // Audio clips for different sounds:
    public AudioClip clickSound;
    public AudioClip deathSound;
    public AudioClip pickupSound;

    // Background music for levels:
    public AudioClip scene1Music;
    public AudioClip scene2Music;

    private AudioSource sfxAudioSource;
    private AudioSource bgmAudioSource;

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            sfxAudioSource = gameObject.AddComponent<AudioSource>();
            bgmAudioSource = gameObject.AddComponent<AudioSource>();

            bgmAudioSource.volume = 0.2f;
            bgmAudioSource.loop = true;
        }
        else
        {
            // If an instance already exists and it's not this one, destroy this one.
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Play appropriate level music when a new scene is loaded.
        PlayLevelMusic(scene.buildIndex);
    }

    public void PlayClickSound()
    {
        PlaySound(clickSound);
    }

    public void PlayDeathSound()
    {
        PlaySound(deathSound);
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }


    public void PlayLevelMusic(int level)
    {
        // Reset
        bgmAudioSource.Stop();
        AudioClip levelMusic = GetLevelMusic(level);

        if (levelMusic != null)
        {
            bgmAudioSource.clip = levelMusic;
            bgmAudioSource.Play();
        }
        else
        {
            Debug.Log("Level music not found for level " + level);
        }
    }

    public void StopLevelMusic()
    {
        bgmAudioSource.Stop();
    }

    private void PlaySound(AudioClip clip)
    {
        if (sfxAudioSource.isPlaying)
        {
            sfxAudioSource.Stop();
        }
        sfxAudioSource.PlayOneShot(clip);
    }

    private AudioClip GetLevelMusic(int level)
    {
        switch (level)
        {
            case 1:
                return scene1Music;
            case 2:
                return scene2Music;
            default:
                return null;
        }
    }
}

