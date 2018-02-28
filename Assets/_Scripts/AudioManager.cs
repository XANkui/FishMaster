using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    //创建音频管理器的单例模式
    private static AudioManager _instance;
    public static AudioManager Instance {
        get {
            return _instance;
        }
    }

    public AudioSource audioSource;
    public AudioClip seaWaveAudio;
    public AudioClip rewardAudio;
    public AudioClip lvUpAudio;
    public AudioClip fireAudio;
    public AudioClip changeGunAudio;
    public AudioClip jinShaAudio;
    public AudioClip yinshaAudio;

    private bool isMute = false;
    public bool IsMute {
        get {
            return isMute;
        }
    }

    private void Awake()
    {
        _instance = this;
        isMute = (PlayerPrefs.GetInt("ismute") == 0) ? false : true;
        DoMute();
    }

    /// <summary>
    /// 是否静音
    /// </summary>
    public void SwitchMuteIsOrNot(bool isOn) {
        isMute = !isOn;
        DoMute();
    }

    private void DoMute() {
        if (isMute == true)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioClip"></param>
    public void PlayEffectAudio(AudioClip audioClip) {
        if (isMute == false) {
            AudioSource.PlayClipAtPoint(audioClip, new Vector3(0,0,-8));
        } 
    }
}
