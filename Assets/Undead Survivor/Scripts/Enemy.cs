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
    Collider2D eColl;
    SpriteRenderer eSprite;
    Animator eAnim;
    WaitForFixedUpdate wait;

    void Awake()
    {
        eRigid = GetComponent<Rigidbody2D>();
        eColl = GetComponent<Collider2D>();
        eSprite = GetComponent<SpriteRenderer>();
        eAnim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if (!GameManager.instace.isLive)
            return;

        if (!isLive || eAnim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        Vector2 dirVec = eTarget.position - eRigid.position;
        Vector2 nextVec = dirVec.normalized * eSpeed * Time.fixedDeltaTime;
        eRigid.MovePosition(eRigid.position + nextVec);
        eRigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!GameManager.instace.isLive)
            return;

        if (!isLive)
            return;

        eSprite.flipX = eTarget.position.x < eRigid.position.x ? true : false;

    }
    void OnEnable()
    {
        eTarget = GameManager.instace.player.GetComponent<Rigidbody2D>();
        isLive = true;
        eColl.enabled = true;
        eRigid.simulated = true;
        eSprite.sortingOrder = 2;
        eAnim.SetBool("Dead", false);
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
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        eHealth -= collision.GetComponent<Bullet>().bDamage;
        StartCoroutine(KnockBack());


        if (eHealth > 0)
        {
            eAnim.SetTrigger("Hit");
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);

        }
        else
        {
            isLive = false;
            eColl.enabled = false;
            eRigid.simulated = false;
            eSprite.sortingOrder = 1;
            eAnim.SetBool("Dead", true);

            GameManager.instace.kill++;
            GameManager.instace.GetExp();

            if (GameManager.instace.isLive)
            {
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            }

        }
    }

    
    IEnumerator KnockBack()
    {
        yield return wait; //하나의 물리 프레임 딜레이

        Vector3 playerPos = GameManager.instace.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        eRigid.AddForce(dirVec.normalized*5, ForceMode2D.Impulse);
    }


    void Dead()
    {
        gameObject.SetActive(false);
    }
}
