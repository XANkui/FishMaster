using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : MonoBehaviour {

    public Toggle isMuteToggle;
    public GameObject settingPanel;

    private void Start()
    {
        isMuteToggle.isOn = !AudioManager.Instance.IsMute;
    }

    /// <summary>
    /// 是否静音
    /// </summary>
    public void SwitchIsMuteOrNot(bool isOn) {
        AudioManager.Instance.SwitchMuteIsOrNot(isOn);
    }

    /// <summary>
    /// 返回按钮事件，并把保存游戏
    /// </summary>
    public void OnBackButtonDown() {

        PlayerPrefs.SetInt("gold", GameController.Instance.gold);
        PlayerPrefs.SetInt("lv", GameController.Instance.lv);
        PlayerPrefs.SetFloat("scd", GameController.Instance.smallTimer);
        PlayerPrefs.SetFloat("bcd", GameController.Instance.bigTimer);
        PlayerPrefs.SetInt("exp", GameController.Instance.exp);
        int temp = (AudioManager.Instance.IsMute == false) ? 0 : 1;
        PlayerPrefs.SetInt("ismute", temp);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    /// <summary>
    /// 设置按钮事件
    /// </summary>
    public void OnSettingButtonDown() {
        settingPanel.SetActive(true);
    }

    /// <summary>
    /// 关闭设置面板事件
    /// </summary>
    public void CloseSettingPanel() {
        settingPanel.SetActive(false);
    }
}
