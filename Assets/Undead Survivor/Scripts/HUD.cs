using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health
    }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake()
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    //연산이 끝날때
    void LateUpdate()
    {
        switch (type) 
        {
            case InfoType.Exp:
                //GamaManager의 Exp변동에 따라 Slider 조정
                float curExp = GameManager.instace.exp;
                float maxExp = GameManager.instace.nextExp[GameManager.instace.level];
                mySlider.value = curExp / maxExp;

                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}", GameManager.instace.level);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}", GameManager.instace.kill);
                break;
            case InfoType.Time:
                float remainTime = GameManager.instace.maxGameTime - GameManager.instace.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = string.Format("{0:D2}:{1:D2}", min, sec);
                break;
            case InfoType.Health:
                float curHealth = GameManager.instace.health;
                float maxHealth = GameManager.instace.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
        
    }
}
                                                         