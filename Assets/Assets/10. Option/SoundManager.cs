using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

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

    private float masterVol = 1;
    private float bgVol = 1;
    private float sfxVol = 1;

    private float fixMasterVol = 1;
    private float fixBgVol = 1;
    private float fixSfxVol = 1;

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

    public void SetVolume(float master, float bg, float sfx)
    {
        masterVol = master;
        fixMasterVol = master;

        bgVol = bg;
        fixBgVol = bg;

        sfxVol = sfx;
        fixSfxVol = sfx;

        mixer.SetFloat("Master", masterVol != 0 ? Mathf.Log10(masterVol) * 20 : -60f);
        mixer.SetFloat("BGVolume", bgVol != 0 ? Mathf.Log10(bgVol) * 20 : -60f);
        mixer.SetFloat("SFXVolume", sfxVol != 0 ? Mathf.Log10(sfxVol) * 20 : -60f);
    }

    public void MasterChanged(float value)
    {
        fixMasterVol = value;
    }

    public void BGSoundChanged(float value)
    {
        fixBgVol = value;
    }

    public void SFXChanged(float value)
    {
        fixSfxVol = value;
    }

    public void VolumeConfirm()
    {
        masterVol = fixMasterVol;
        bgVol = fixBgVol;
        sfxVol = fixSfxVol;

        PlayerPrefs.SetFloat("Master", masterVol);
        PlayerPrefs.SetFloat("BG", bgVol);
        PlayerPrefs.SetFloat("SFX", sfxVol);

        mixer.SetFloat("Master", masterVol != 0 ? Mathf.Log10(masterVol) * 20 : -60f);
        mixer.SetFloat("BGVolume", bgVol != 0 ? Mathf.Log10(bgVol) * 20 : -60f);
        mixer.SetFloat("SFXVolume", sfxVol != 0 ? Mathf.Log10(sfxVol) * 20 : -60f);
    }

    public void VolumeCancel()
    {
        mixer.SetFloat("Master", masterVol != 0 ? Mathf.Log10(masterVol) * 20 : -60f);
        mixer.SetFloat("BGVolume", bgVol != 0 ? Mathf.Log10(bgVol) * 20 : -60f);
        mixer.SetFloat("SFXVolume", sfxVol != 0 ? Mathf.Log10(sfxVol) * 20 : -60f);
    }
}
