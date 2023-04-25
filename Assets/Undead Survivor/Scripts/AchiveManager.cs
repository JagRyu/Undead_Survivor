using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject uiNotice;
    WaitForSecondsRealtime wait;

    enum Achive
    {
        UnlockPotato,
        UnlockBean
    }
    Achive[] achives;

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(3f);
        // 처음 실행하는 사람만 초기화 됨.
        if (!PlayerPrefs.HasKey("MyData"))
        {
            Init();
        }
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);

        foreach (var achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
        
    }

    void Start()
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for(int index = 0; index<lockCharacter.Length; index++)
        {
            string achiveName = achives[index].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[index].SetActive(!isUnlock);
            unlockCharacter[index].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach(var achive in achives)
        {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockPotato:
                isAchive = GameManager.instace.kill >= 10;
                break;
            case Achive.UnlockBean:
                isAchive = GameManager.instace.gameTime == GameManager.instace.maxGameTime;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
            for(int index = 0; index< uiNotice.transform.childCount; index++)
            {
                bool isActive = index == (int)achive;
                uiNotice.transform.GetChild(index).gameObject.SetActive(isActive);
            }

            StartCoroutine(NoticRoutine());
        }
    }

    IEnumerator NoticRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);

        yield return wait;

        uiNotice.SetActive(false);
    }
}
