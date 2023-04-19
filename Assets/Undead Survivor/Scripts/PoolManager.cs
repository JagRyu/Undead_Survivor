using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // #01. �����յ��� ������ ����
    public GameObject[] prefabs;
    // #02. Ǯ ����� �ϴ� ����Ʈ��
    List<GameObject>[] pools;
    // ���� ������ 1:1 �Դϴ�.

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int index = 0; index<pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;
        // #03. ������ Ǯ�� ��� �ִ�(��Ȱ��ȭ ��) GameObject ����
        // #03-1 �߰��ϸ� select ������ �Ҵ�
        foreach(GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }
        if(select == null)
        {
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        // #03-2 ��� ������̸�(��� Ȱ��ȭ ��)? => ���Ӱ� �����ϰ� select ������ �Ҵ� 

        return select;
    }


}
