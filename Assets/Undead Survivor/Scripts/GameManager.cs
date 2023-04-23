using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //singleTone X , �޸𸮿� �ø���.
    public static GameManager instace;

    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public int health;
    public int maxHealth = 100;
    public int[] nextExp = { 3,5, 10, 100, 150, 210, 280, 360, 450, 600 };

    void Awake()
    {
        instace = this;
    }
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }

}
