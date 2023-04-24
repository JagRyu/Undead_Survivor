using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //singleTone X , �޸𸮿� �ø���.
    public static GameManager instace;

    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;
    public LevelUp uiLevelUp;
    public Result uiResult;
    public GameObject enemyCleaner;

    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    public bool isLive;

    [Header("# Player Info")]
    public int level;
    public int kill;
    public int exp;
    public float health;
    public float maxHealth = 100;
    public int[] nextExp = { 3,5, 10, 100, 150, 210, 280, 360, 450, 600 };

    void Awake()
    {
        instace = this;
    }
    public void GameStart()
    {
        health = maxHealth;
        //�ӽ��ڵ� �⺻����
        uiLevelUp.Select(1); //���� 

        isLive = true;
        Resume();

    }
    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
    }
    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();
    }
    public void GameReTry()
    {
        SceneManager.LoadScene("MainScene");
    }

    void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive)
            return;


        exp++;

        if(exp == nextExp[Mathf.Min(level, nextExp.Length-1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false; // update()�Լ� ���� �ֵ����� �� �������
        Time.timeScale = 0;
            //time Scale : ����Ƽ�� �ð� �ӵ�(����) 2�� �ָ� 2��� �� �̷���� ����
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }
}
