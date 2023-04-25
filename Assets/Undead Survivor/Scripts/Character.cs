using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public static float Speed
    {
        get { return GameManager.instace.playerId == 0 ? 1.1f : 1f; }

    }
    public static float WeaponSpeed // ���� �����
    {
        get { return GameManager.instace.playerId == 1 ? 1.1f : 1f; }
    }
    public static float WeaponRate // ���Ÿ� �����
    {
        get { return GameManager.instace.playerId == 1 ? 0.9f : 1f; }
    }
    public static float Damage
    {
        get { return GameManager.instace.playerId == 2 ? 1.2f : 1f; }

    }
    public static float Count
    {
        get { return GameManager.instace.playerId == 3 ? 1 : 0; }

    }

}
