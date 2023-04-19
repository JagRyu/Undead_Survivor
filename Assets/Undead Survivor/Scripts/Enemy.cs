using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float eSpeed;
    public Rigidbody2D eTarget;

    bool isLive = true;
    Rigidbody2D eRigid;
    SpriteRenderer eSprite;

    void Awake()
    {
        eRigid = GetComponent<Rigidbody2D>();
        eSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        Vector2 dirVec = eTarget.position - eRigid.position;
        Vector2 nextVec = dirVec.normalized * eSpeed * Time.fixedDeltaTime;
        eRigid.MovePosition(eRigid.position + nextVec);
        eRigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive)
            return;

        eSprite.flipX = eTarget.position.x < eRigid.position.x ? true : false;

    }
    void OnEnable()
    {
        eTarget = GameManager.instace.player.GetComponent<Rigidbody2D>();
    }
}
