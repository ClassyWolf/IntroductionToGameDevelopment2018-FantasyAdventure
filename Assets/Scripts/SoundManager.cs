using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public AudioSource mainTheme;
    public AudioSource forrestTheme;
    public AudioSource bossTheme;
    public AudioSource efxSource;
    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SwapMusic(scene.name);
    }

    void SwapMusic(string name)
    {
        if(name == "MainMenu")
        {
            mainTheme.Play();
            forrestTheme.Stop();
            bossTheme.Stop();
        }
        else if(name == "Map1")
        {
            forrestTheme.Play();
            mainTheme.Stop();
            bossTheme.Stop();
        }
        else if(name == "TestRoom")
        {
            bossTheme.Play();
            mainTheme.Stop();
            forrestTheme.Stop();
        }
    }

    public void PlayJump(AudioClip clip)
    {
        if(efxSource.isPlaying == false)
        {
            efxSource.clip = clip;
            efxSource.PlayOneShot(clip);
        }   
    }

    public void PlayMove(AudioClip clip)
    {
        if (efxSource.isPlaying == false)
        {
            efxSource.clip = clip;
            efxSource.PlayOneShot(clip);
        }
    }

    public void HookShot(AudioClip clip)
    {
        if (efxSource.isPlaying == false)
        {
            efxSource.clip = clip;
            efxSource.PlayOneShot(clip);
        }
    }

    public void HookRelease(AudioClip clip)
    {
        if (efxSource.isPlaying == false)
        {
            efxSource.clip = clip;
            efxSource.PlayOneShot(clip);
        }
    }

    public void PlayDmg(AudioClip clip)
    {
        if (efxSource.isPlaying == false)
        {
            efxSource.clip = clip;
            efxSource.PlayOneShot(clip);
        }
    }
}
