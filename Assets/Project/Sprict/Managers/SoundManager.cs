using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmAudioSource;
    [SerializeField]
    AudioSource bgmAudioSource2;
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
    public float BgmVolume2
    {
        get
        {
            return bgmAudioSource2.volume;
        }
        set
        {
            bgmAudioSource2.volume = Mathf.Clamp01(value);
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
    }

    GameObject CheckOtherSoundManager()
    {
        return GameObject.FindGameObjectWithTag("SoundManager");
    }

    public void PlayBgm(AudioClip clip)
    {
        if (bgmAudioSource.clip == clip || bgmAudioSource.clip == clip) return;
        bool bgmSwitch = false;
        if (bgmAudioSource.clip != null)
        {
            bgmAudioSource2.clip = clip;
        }
        else
        {
            bgmAudioSource.clip = clip;
            bgmSwitch = true;
        }

        if(clip == null)
        {
            return;
        }

        if(bgmSwitch == true)bgmAudioSource.Play();
        bgmSwitch = false;
        if(bgmAudioSource2.clip != null) bgmAudioSource2.Play();
    }
    public void StopBgm(AudioClip clip)
    {
        Debug.Log(clip);
        if(bgmAudioSource.clip != clip)
        {
            Debug.Log("ちがうよ１");
            if (bgmAudioSource2.clip == clip)
            {
                Debug.Log("けすよ2");
                bgmAudioSource2.Stop();
                bgmAudioSource2.clip = null;
            }
            Debug.Log("ちがうよ2");
            return;
        }
        Debug.Log("けすよ１");
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
        
    }

    public void PlaySe(AudioClip clip)
    {
        Debug.Log(seAudioSource);
        if(clip == null)
        {
            return;
        }

        seAudioSource.PlayOneShot(clip);
    }
    public void StopSe(AudioClip clip)
    {
        Debug.Log(seAudioSource);
        Debug.Log(clip);
        Debug.Log("aaaa");
        seAudioSource.clip = clip;

        if (clip == null)
        {
            return;
        }

        seAudioSource.Stop();
        seAudioSource.clip = null;
    }

    /// <summary>
    /// スライドバー値の変更イベント
    /// </summary>
    /// <param name="newSliderValue">スライドバーの値(自動的に引数に値が入る)</param>
    public void BGMSoundSliderOnValueChange(float newSliderValue)
    {
        // 音楽の音量をスライドバーの値に変更
        bgmAudioSource.volume = newSliderValue;
        bgmAudioSource2.volume = newSliderValue;
    }
    public void SESoundSliderOnValueChange(float newSliderValue)
    {
        // 音楽の音量をスライドバーの値に変更
        seAudioSource.volume = newSliderValue;
    }
}
