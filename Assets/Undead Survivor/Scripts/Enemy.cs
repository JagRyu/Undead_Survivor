using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float eSpeed;
    public float eHealth;
    public float eMaxHealth;
    public RuntimeAnimatorController[] eAnimCon;
    public Rigidbody2D eTarget;

    bool isLive;
    Rigidbody2D eRigid;
    SpriteRenderer eSprite;
    Animator eAnim;

    void Awake()
    {
        eRigid = GetComponent<Rigidbody2D>();
        eSprite = GetComponent<SpriteRenderer>();
        eAnim = GetComponent<Animator>();
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
        isLive = true;
        eHealth = eMaxHealth;
    }

    public void GetSpawnData(SpawnData data)
    {
        eAnim.runtimeAnimatorController = eAnimCon[data.spriteType];
        eSpeed = data.eSpeed;
        eMaxHealth = data.eHealth;
        eHealth = data.eHealth;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        eHealth -= collision.GetComponent<Bullet>().bDamage;

        if(eHealth > 0)
        {

        }
        else
        {
            Dead();
        }

        
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
