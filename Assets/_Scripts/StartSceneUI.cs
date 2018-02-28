using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneUI : MonoBehaviour {

    public GameObject playGameButton;
    public GameObject resumeGameButton;
    public GameObject loadPanel;
    public Slider loadSlider;
    public Text progressText;

    //异步加载操作符
    private AsyncOperation async;
    private int curProgressVaule = 0;//计数器

    void Update()
    {

        if (async == null)
        {
            return;
        }

        int progressVaule = 0;

        if (async.progress < 0.9f)
        {
            progressVaule = (int)async.progress * 100;
        }
        else
        {
            progressVaule = 100;
        }

        if (curProgressVaule < progressVaule)
        {
            curProgressVaule++;
        }
        progressText.text = curProgressVaule + "%";
        //progressImg.fillAmount = curProgressVaule / 100f;
        loadSlider.value = curProgressVaule / 100f;
        if (curProgressVaule == 100)
        {
            async.allowSceneActivation = true;
        }
    }

    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void NewGame() {
        HideOrShowUI();
        PlayerPrefs.DeleteKey("gold");
        PlayerPrefs.DeleteKey("lv");
        PlayerPrefs.DeleteKey("exp");
        PlayerPrefs.DeleteKey("scd");
        PlayerPrefs.DeleteKey("bcd");
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void OldGame()
    {
        HideOrShowUI();
        //SceneManager.LoadScene(1);
        StartCoroutine(LoadScene());
    }

    /// <summary>
    /// 显示隐藏UI
    /// </summary>
    private void HideOrShowUI() {
        playGameButton.SetActive(false);
        resumeGameButton.SetActive(false);
        loadPanel.SetActive(true);

    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(1);//异步跳转到game场景
        async.allowSceneActivation = false;//当game场景加载到90%时，不让它直接跳转到game场景。
        yield return async;
    }
}
