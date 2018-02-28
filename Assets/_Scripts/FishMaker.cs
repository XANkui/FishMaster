using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour {

    public Transform fishHolder;
    public Transform[] generatePos;
    public GameObject[] fishPrefabs;

    public float fishGenWaitTime = 0.5f;
    public float waveGenWaitTime = 0.5f;
	// Use this for initialization
	void Start () {
        InvokeRepeating("MakeFishes", 0, waveGenWaitTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void MakeFishes() {
        int genPosIndex = Random.Range(0, generatePos.Length);
        int fishPreIndex = Random.Range(0, fishPrefabs.Length);
        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAtr>().maxNum;
        int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAtr>().maxSpeed;
        int num = Random.Range((maxNum / 2), maxNum + 1);
        int speed = Random.Range((maxSpeed / 2) , maxSpeed + 1);

        int moveType = Random.Range(0, 2); //0位直走，1为转弯
        int angOffset;  //直走角度
        int angSpeed;   //转弯速度
        if (moveType == 0)
        {
            angOffset = Random.Range(-22, 22);
            StartCoroutine(GenStraightFish(genPosIndex, fishPreIndex, num, speed, angOffset));
        }
        else {
            if (Random.Range(0, 2) == 0)
            {
                angSpeed = Random.Range(-15, -10);
            }
            else {
                angSpeed = Random.Range(10, 15);
            }

            StartCoroutine(GenTurnFish(genPosIndex, fishPreIndex, num, speed, angSpeed));
        }
    }

    IEnumerator GenStraightFish(int genPosIndex, int fishPreIndex, int num, int speed, int angOffset) {

        for (int i =0; i < num; i ++) {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = generatePos[genPosIndex].localPosition ;
            fish.transform.localRotation = generatePos[genPosIndex].localRotation;
            fish.transform.Rotate(0, 0, angOffset);
            fish.AddComponent<EffectAutoMove>().speed = speed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }

    IEnumerator GenTurnFish(int genPosIndex, int fishPreIndex, int num, int speed, int angSpeed)
    {

        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = generatePos[genPosIndex].localPosition;
            fish.transform.localRotation = generatePos[genPosIndex].localRotation;
            fish.AddComponent<EffectAutoMove>().speed = speed;
            fish.AddComponent<EffectAutoRotate>().speed = angSpeed;
            yield return new WaitForSeconds(fishGenWaitTime);
        }
    }
}
