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
    /// �X���C�h�o�[�l�̕ύX�C�x���g
    /// </summary>
    /// <param name="newSliderValue">�X���C�h�o�[�̒l(�����I�Ɉ����ɒl������)</param>
    public void BGMSoundSliderOnValueChange(float newSliderValue)
    {
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        bgmAudioSource.volume = newSliderValue;
    }
    public void SESoundSliderOnValueChange(float newSliderValue)
    {
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        seAudioSource.volume = newSliderValue;
    }
}
