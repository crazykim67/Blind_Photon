using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    #region Instance

    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    public AudioMixer mixer;
    public AudioSource bgSound;
    public AudioClip[] bgClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
            Destroy(gameObject);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        for (int i = 0; i < bgClips.Length; i++)
        {
            if (arg0.name == bgClips[i].name)
            {
                BgSoundPlay(bgClips[i]);
                return;
            }
        }
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName);
        AudioSource ad = go.AddComponent<AudioSource>();
        ad.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        ad.clip = clip;
        ad.Play();

        Destroy(go, clip.length);
    }

    public void BgSoundPlay(AudioClip clip)
    {
        bgSound.outputAudioMixerGroup = mixer.FindMatchingGroups("BGSound")[0];
        bgSound.clip = clip;
        bgSound.loop = true;
        bgSound.volume = 0.1f;
        bgSound.Play();
    }

    public void BGSoundChanged(float value)
    {
        mixer.SetFloat("BGVolume", Mathf.Log10(value) * 20);
    }

    public void SFXChanged(float value)
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}
