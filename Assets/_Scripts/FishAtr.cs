
using UnityEngine;

class FishAtr : MonoBehaviour
{
    public int hp;
    public int exp = 30;
    public int gold = 30;
    public int maxNum;
    public int maxSpeed;
    public GameObject diePrefab;
    public GameObject goldPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Border"))
        {
            Destroy(gameObject);
            print("Destroy");
        }
    }

    /// <summary>
    /// 鱼受到伤害
    /// </summary>
    /// <param name="damage"></param>
    public void TakeDamage(int damage) {
        if (hp <= 0) {
            //鱼死亡获得金币与经验值
            GameController.Instance.gold += gold;
            GameController.Instance.exp += exp;
            //生成鱼死亡的预制体，并设置父物体，位置与方向
            GameObject dieGo = Instantiate(diePrefab);
            dieGo.transform.SetParent(transform.parent, false);
            dieGo.transform.position = transform.position;
            dieGo.transform.rotation = transform.rotation;
            //生成金币
            GameObject goldGo = Instantiate(goldPrefab);
            goldGo.transform.SetParent(transform.parent, false);
            goldGo.transform.position = transform.position;
            goldGo.transform.rotation = transform.rotation;
            //播放特效
            if (gameObject.GetComponent<EffectPlayEffect>() != null) {
                gameObject.GetComponent<EffectPlayEffect>().PlayEffcet();
            }
            //销毁自身
            Destroy(gameObject);
        }

        hp -= damage;
    }
}

