using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleTone X , 메모리에 올린다.
    public static GameManager instace;
    public Player player;
    public PoolManager pool;

    void Awake()
    {
        instace = this;
    }
}
