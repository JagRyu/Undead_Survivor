using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ������
    public float bDamage;
    // �����
    public int bPer;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    public void Init(float damage, int per, Vector3 dir)
    {
        this.bDamage = damage;
        this.bPer = per;

        if(per > -1)
        {
            rigid.velocity = dir*15f;
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || bPer == -1)
            return;
        bPer--;

        if(bPer == -1)
        {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }

    }
}
