using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft; // true = left, false = right;
    public SpriteRenderer spriter;

    SpriteRenderer playerSprite;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -15);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -115);

    void Awake()
    {
        playerSprite = GetComponentsInParent<SpriteRenderer>()[1];
    }
    void LateUpdate()
    {
        bool isReverse = playerSprite.flipX;
        if (isLeft)
        {
            //근거리
            transform.localRotation = isReverse ? leftRotReverse: leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ?4:6;
        }
        else
        {
            //원거리
            transform.localPosition = isReverse ?rightPosReverse:rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse?6:4;
        }
    }
}
