using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    //UI�� Rectransform
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);
    }

    public void Show() // LevelUp ������
    {
        Next();
        GameManager.instace.Stop();
        rect.localScale = Vector3.one;

        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);

    }

    public void Hide() // Button �ۿ��Ҷ� 
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
        // 1. ��� ������ ��Ȱ��ȭ
        foreach(var item in items)
        {
            item.gameObject.SetActive(false);

        }
        // 2. �� �߿��� �����ϰ� 3�� �����۸� Ȱ��ȭ
        int[] rans = new int[3];
        //while true�� ���ѷ����̱� ������ if-break�� ���������� ���� �� ������ֱ�
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
            // 3. ���� ������ ��� �Һ� ���������� ��ü
            if (ranItem.level == ranItem.data.damages.Length)
            {
                // item 4�ϳ� �ۿ� ������
                items[4].gameObject.SetActive(true);
                // �������� random.Range(4,-).gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
