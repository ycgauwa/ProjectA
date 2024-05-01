using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmAudioSource;
    [SerializeField]
    AudioSource seAudioSource;

    public float BgmVolume
    {
        get
        {
            return bgmAudioSource.volume;
        }
        set
        {
            bgmAudioSource.volume = Mathf.Clamp01(value);
        }
    }

    public float SeVolume
    {
        get
        {
            return seAudioSource.volume;
        }
        set
        {
            seAudioSource.volume = Mathf.Clamp01(value);
        }
    }


    void Start()
    {
        GameObject soundManager = CheckOtherSoundManager();
        bool checkResult = soundManager != null && soundManager != gameObject;

        //if(checkResult)
        //{
        //    Destroy(gameObject);
        //}

        //DontDestroyOnLoad(gameObject);
    }

    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void PlayBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;

        if(clip == null)
        {
            return;
        }

        bgmAudioSource.Play();
    }
    public void StopBgm(AudioClip clip)
    {
        bgmAudioSource.clip = clip;

        if(clip == null)
        {
            return;
        }

        bgmAudioSource.Stop();
    }

    public void PlaySe(AudioClip clip)
    {
        if(clip == null)
        {
            return;
        }

        seAudioSource.PlayOneShot(clip);
    }

    /// <summary>
    /// スライドバー値の変更イベント
    /// </summary>
    /// <param name="newSliderValue">スライドバーの値(自動的に引数に値が入る)</param>
    public void BGMSoundSliderOnValueChange(float newSliderValue)
    {
        // 音楽の音量をスライドバーの値に変更
        bgmAudioSource.volume = newSliderValue;
    }
    public void SESoundSliderOnValueChange(float newSliderValue)
    {
        // 音楽の音量をスライドバーの値に変更
        seAudioSource.volume = newSliderValue;
    }
}
