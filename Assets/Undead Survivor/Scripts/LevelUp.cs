using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    //UI는 Rectransform
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show() // LevelUp 했을때
    {
        Next();
        GameManager.instace.Stop();
        rect.localScale = Vector3.one;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);

    }

    public void Hide() // Button 작용할때 
    {
        GameManager.instace.Resume();
        rect.localScale = Vector3.zero;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);

    }

    public void Select(int index)
    {
        items[index].OnClick();
    }

    void Next()
    {
        // 1. 모든 아이템 비활성화
        foreach(var item in items)
        {
            item.gameObject.SetActive(false);

        }
        // 2. 그 중에서 랜덤하게 3개 아이템만 활성화
        int[] rans = new int[3];
        //while true는 무한루프이기 때문에 if-break로 빠져나가는 조건 꼭 만들어주기
        while (true)
        {
            rans[0] = Random.Range(0, items.Length);
            rans[1] = Random.Range(0, items.Length);
            rans[2] = Random.Range(0, items.Length);


            if (rans[0]!=rans[1] && rans[0]!=rans[2] && rans[1]!=rans[2])
                break;
        }
        

        for(int index = 0; index<rans.Length; index++)
        {
            Item ranItem = items[rans[index]];
            // 3. 만렙 아이템 경우 소비 아이템으로 대체
            if (ranItem.level == ranItem.data.damages.Length)
            {
                // item 4하나 밖에 없긴해
                items[4].gameObject.SetActive(true);
                // 여러개면 random.Range(4,-).gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
