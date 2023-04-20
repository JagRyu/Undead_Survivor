using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleTone X , �޸𸮿� �ø���.
    public static GameManager instace;
    public Player player;
    public PoolManager pool;

    public float gameTime;
    public float maxGameTime = 2 * 10f;

    void Awake()
    {
        instace = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }
}
