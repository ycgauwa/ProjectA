using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioSource bgmAudioSource;
    [SerializeField]
    AudioSource bgmAudioSource2;
    [SerializeField]
    AudioSource seAudioSource;
    public Slider bgmSlider;
    public Slider seSlider;
    public static SoundManager sound_Instance;


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

    private void Awake()
    {
        if(sound_Instance == null)
        {
            sound_Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        bgmAudioSource.volume = PlayerPrefs.GetFloat("bgmvolume",0.5f);
        bgmAudioSource2.volume = PlayerPrefs.GetFloat("bgmvolume", 0.5f);
        seAudioSource.volume = PlayerPrefs.GetFloat("sevolume", 0.5f);
        bgmSlider.value = bgmAudioSource.volume;
        seSlider.value = seAudioSource.volume;
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
            Debug.Log("��������P");
            if (bgmAudioSource2.clip == clip)
            {
                Debug.Log("������2");
                bgmAudioSource2.Stop();
                bgmAudioSource2.clip = null;
            }
            Debug.Log("��������2");
            return;
        }
        Debug.Log("������P");
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }
    public void PauseBgm(AudioClip clip)
    {
        if(bgmAudioSource.clip != clip)
        {
            if(bgmAudioSource2.clip == clip)
            {
                bgmAudioSource2.Pause();
            }
            return;
        }
        bgmAudioSource.Pause();
    }
    public void UnPauseBgm(AudioClip clip)
    {
        if(bgmAudioSource.clip != clip)
        {
            if(bgmAudioSource2.clip == clip)
            {
                bgmAudioSource2.UnPause();
            }
            return;
        }
        bgmAudioSource.UnPause();
    }

    public void PlaySe(AudioClip clip)
    {
        Debug.Log(clip);
        if(clip == null)
        {
            return;
        }
        seAudioSource.PlayOneShot(clip);
    }
    public void StopSe(AudioClip clip)
    {
        seAudioSource.clip = clip;
        if (clip == null)
        {
            return;
        }
        seAudioSource.Stop();
        seAudioSource.clip = null;
    }

    /// <summary>
    /// �X���C�h�o�[�l�̕ύX�C�x���g
    /// </summary>
    /// <param name="newSliderValue">�X���C�h�o�[�̒l(�����I�Ɉ����ɒl������)</param>
    public void BGMSoundSliderOnValueChange(float newSliderValue)
    {
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX(�ǂ���������X���C�h�o�[)
        bgmAudioSource.volume = newSliderValue;
        bgmAudioSource2.volume = newSliderValue;
        PlayerPrefs.SetFloat("bgmvolume", bgmAudioSource.volume);
    }
    public void SESoundSliderOnValueChange(float newSliderValue)
    {
        // ���y�̉��ʂ��X���C�h�o�[�̒l�ɕύX
        seAudioSource.volume = newSliderValue;
        PlayerPrefs.SetFloat("sevolume", seAudioSource.volume);
    }
}
