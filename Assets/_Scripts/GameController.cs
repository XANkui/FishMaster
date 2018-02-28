using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{

    //单例化GameController脚本
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    //花费的UI文本
    public Text onShotCost;

    public Text goldText;
    public Color goldTextOriginColor;
    public Text lvText;
    public Text lvNameText;
    public Text smallCountDownText;
    public Text bigCountDownText;
    public Button bigCountDownButton;
    public Button backButton;
    public Button settingButton;
    public Slider expSlider;

    public int lv = 0;
    public int exp = 0;
    public int gold = 500;
    public const int bigCountDownTime = 240;
    public const int smallCountDownTime = 60;
    public float bigTimer = bigCountDownTime;
    public float smallTimer = smallCountDownTime;

    public GameObject lvUpTip;
    public GameObject lvUpEffect;
    public GameObject gunChangeEffect;
    public GameObject gunFireEffect;
    //每25级更新背景，以及生成海浪
    public Image bgImage;
    public Sprite[] bgSpriteArray;
    private int bgSpriteIndex = 0;
    public GameObject seaWavePrefab;



    //不同枪的数组
    public GameObject[] gunGoes;
    private int costIndex = 0;
    //枪的子弹花费数组
    private int[] shotCostArray = {5, 10, 20, 30, 40, 50, 60 ,70 ,80 ,90 ,100,
        200, 300, 400, 500, 600, 700, 800, 900, 1000 };

    private string[] lvNameArray = { "新手", "入门", "钢铁", "青铜", "白银", "黄金", "铂金", "钻石", "大师", "宗师" };

    //不同枪的子弹数组
    public GameObject[] bullet1Goes;
    public GameObject[] bullet2Goes;
    public GameObject[] bullet3Goes;
    public GameObject[] bullet4Goes;
    public GameObject[] bullet5Goes;
    //生成的子弹父物体
    public Transform bulletHolder;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        //更新初始花费
        onShotCost.text = "$" + shotCostArray[costIndex].ToString();
        goldTextOriginColor = goldText.color;

        //加载游戏保存的值，没有只使用默认值
        gold = PlayerPrefs.GetInt("gold", gold);
        lv = PlayerPrefs.GetInt("lv", lv);
        exp = PlayerPrefs.GetInt("exp", exp);
        smallTimer = PlayerPrefs.GetFloat("scd", smallCountDownTime);
        bigTimer = PlayerPrefs.GetFloat("bcd", bigCountDownTime);
        UpdateUI();
    }

    private void Update()
    {

        ShotChangeCost();
        Fire();
        UpdateUI();
        ChangeBgImage();
    }

    /// <summary>
    /// 经验、等级、金币等的UI更新
    /// </summary>
    private void UpdateUI()
    {

        bigTimer -= Time.deltaTime;
        smallTimer -= Time.deltaTime;
        if (bigTimer <= 0)
        {
            bigCountDownText.gameObject.SetActive(false);
            bigCountDownButton.gameObject.SetActive(true);
        }
        if (smallTimer <= 0)
        {
            gold += 50;
            smallTimer = smallCountDownTime;
        }

        //使用while可以在经验连升两级时连续调用，鄙视用if好
        //经验计算公式
        while (exp >= (1000 + 200 * lv))
        {
            exp -= 1000 + 200 * lv;
            lv += 1;
            //显示升级特效
            Instantiate(lvUpEffect);
            AudioManager.Instance.PlayEffectAudio(AudioManager.Instance.lvUpAudio);
            lvUpTip.SetActive(true);
            lvUpTip.transform.Find("LvText").GetComponent<Text>().text = lv.ToString();
            StartCoroutine(lvUpTip.transform.GetComponent<EffectHideSelf>().HideSelf(0.5f));
        }

        if (lv >= 100)
        {
            lv = 99;
        }
        lvText.text = lv.ToString();
        lvNameText.text = lvNameArray[lv / 10];
        expSlider.value = ((float)exp / (1000 + 200 * lv));
        goldText.text = gold.ToString();
        bigCountDownText.text = (int)bigTimer + "s";
        smallCountDownText.text = " " + (int)smallTimer / 10 + "  " + (int)smallTimer % 10;
    }

    /// <summary>
    /// 开火
    /// </summary>
    private void Fire()
    {
        GameObject[] bulletGoes = null;
        int bulletIndex;


        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            switch (costIndex / 4)
            {
                case 0: bulletGoes = bullet1Goes; break;
                case 1: bulletGoes = bullet2Goes; break;
                case 2: bulletGoes = bullet3Goes; break;
                case 3: bulletGoes = bullet4Goes; break;
                case 4: bulletGoes = bullet5Goes; break;
            }
            //金币足与不足的逻辑
            if (gold - shotCostArray[costIndex] >= 0)
            {
                bulletIndex = ((lv % 10) >= 9) ? 9 : lv % 10;
                gold -= shotCostArray[costIndex];
                GameObject go = Instantiate(gunFireEffect);
                go.transform.position = gunGoes[costIndex / 4].transform.Find("FirePos").transform.position;
                go.transform.rotation = gunGoes[costIndex / 4].transform.Find("FirePos").transform.rotation;
                AudioManager.Instance.PlayEffectAudio(AudioManager.Instance.fireAudio);
                GameObject bullet = Instantiate(bulletGoes[bulletIndex]);
                bullet.transform.SetParent(bulletHolder, false);
                bullet.transform.position = gunGoes[costIndex / 4].transform.Find("FirePos").transform.position;
                bullet.transform.rotation = gunGoes[costIndex / 4].transform.Find("FirePos").transform.rotation;
                bullet.GetComponent<BulletAttr>().damage = shotCostArray[costIndex];
                bullet.AddComponent<EffectAutoMove>().dir = Vector3.up;
                bullet.GetComponent<EffectAutoMove>().speed = bullet.GetComponent<BulletAttr>().speed;
            }
            else
            {
                //金币不足时文字闪烁
                StartCoroutine(GoldNotEnough());
                print("金币不足时文字闪烁");
            }
        }

    }

    public void ChangeBgImage() {
        //因为UpdateUI中已经限制lv最大99，所以这里lv只会小于4
        if (bgSpriteIndex != (lv / 25)) {
            bgSpriteIndex = lv / 25;
            Instantiate(seaWavePrefab);
            AudioManager.Instance.PlayEffectAudio(AudioManager.Instance.seaWaveAudio);
            bgImage.sprite = bgSpriteArray[bgSpriteIndex];
        }

        
    }

    /// <summary>
    /// 向上换枪
    /// </summary>
    public void OnButtonPDown()
    {
        //播放换枪特效
        GameObject go = Instantiate(gunChangeEffect);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;

        gunGoes[costIndex / 4].SetActive(false);
        costIndex++;
        costIndex = (costIndex >= (shotCostArray.Length)) ? 0 : costIndex;
        gunGoes[costIndex / 4].SetActive(true);
        AudioManager.Instance.PlayEffectAudio(AudioManager.Instance.changeGunAudio);
        onShotCost.text = "$" + shotCostArray[costIndex].ToString();
    }

    /// <summary>
    /// 向下换枪
    /// </summary>
    public void OnButtonMDown()
    {
        //播放换枪特效
        GameObject go = Instantiate(gunChangeEffect);
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;

        gunGoes[costIndex / 4].SetActive(false);
        costIndex--;
        costIndex = (costIndex < 0) ? (shotCostArray.Length - 1) : costIndex;
        gunGoes[costIndex / 4].SetActive(true);
        AudioManager.Instance.PlayEffectAudio(AudioManager.Instance.changeGunAudio);
        onShotCost.text = "$" + shotCostArray[costIndex].ToString();
    }

    /// <summary>
    ///通过滚轮改变花费换枪 
    /// </summary>
    private void ShotChangeCost()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnButtonPDown();
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnButtonMDown();
        }
    }

    /// <summary>
    /// 获取奖金的按钮事件
    /// </summary>
    public void OnBigCountDownButton()
    {
        gold += 500;
        AudioManager.Instance.PlayEffectAudio(AudioManager.Instance.rewardAudio);
        bigCountDownButton.gameObject.SetActive(false);
        bigCountDownText.gameObject.SetActive(true);
        bigTimer = bigCountDownTime;
    }

    /// <summary>
    /// 金币不足，文本一闪一闪
    /// </summary>
    /// <returns></returns>
    private IEnumerator GoldNotEnough()
    {
        
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        goldText.color = goldTextOriginColor;
        yield return new WaitForSeconds(0.2f);
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        goldText.color = goldTextOriginColor;
    }
}
