using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleTone X , �޸𸮿� �ø���.
    public static GameManager instace;
    public Player player;
    public PoolManager pool;

    void Awake()
    {
        instace = this;
    }
}
