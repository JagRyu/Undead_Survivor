using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;

    int level;
    float timer;

    void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
    }


    void Update()
    {
        if (!GameManager.instace.isLive)
            return;

        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instace.gameTime / 10f), spawnData.Length-1);
        if (timer > spawnData[level].spawnTime)
        { 
            timer = 0;
            Spawn();
        }
    }
    void Spawn()
    {
        GameObject enemy = GameManager.instace.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].transform.position;
        enemy.GetComponent<Enemy>().GetSpawnData(spawnData[level]);
    }


}

//소환 데이터 변수로 묶자..
[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int eHealth;
    public float eSpeed;
}

