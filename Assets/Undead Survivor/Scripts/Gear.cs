using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        //Basic Set
        name = "Gear " + data.itemId;
        transform.parent = GameManager.instace.player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }
    
    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    void RateUp()
    {
        //연사력 올리기
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(var weapon in weapons)
        {
            switch (weapon.wId)
            {
                case 0:
                    float speed = -150 * Character.WeaponSpeed;
                    weapon.wSpeed = speed + (speed * rate);
                    break;

                case 1:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.wSpeed = speed * (1f-rate);
                    break;
            }
        }
    }
    void SpeedUp()
    {
        float speed = 4*Character.Speed;
        GameManager.instace.player.pSpeed = speed + speed * rate;
    }
}
