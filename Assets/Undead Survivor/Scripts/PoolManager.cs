using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // #01. 프리팹들을 보관할 변수
    public GameObject[] prefabs;
    // #02. 풀 담당을 하는 리스트들
    List<GameObject>[] pools;
    // 둘의 개수는 1:1 입니다.

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
        // #03. 선택한 풀의 놀고 있는(비활성화 된) GameObject 접근
        // #03-1 발견하면 select 변수에 할당
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
        // #03-2 모두 사용중이면(모두 활성화 된)? => 새롭게 생성하고 select 변수에 할당 

        return select;
    }


}
