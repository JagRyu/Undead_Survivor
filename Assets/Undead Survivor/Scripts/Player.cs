using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 pInputVec;
    public float pSpeed;
    public Scanner scanner;
    public Hand[] hands;


    Rigidbody2D pRigid;
    SpriteRenderer pSpriter;
    Animator pAnim;

    //초기화
    void Awake()
    {
        pRigid = GetComponent<Rigidbody2D>();
        pSpriter = GetComponent<SpriteRenderer>();
        pAnim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        hands = GetComponentsInChildren<Hand>(true);
    }
    void Start()
    {
        
    }

    void Update()
    {
        pInputVec.x = Input.GetAxisRaw("Horizontal");
        pInputVec.y = Input.GetAxisRaw("Vertical");
    }
    void FixedUpdate()
    {
        Vector2 nextVec = pInputVec.normalized * pSpeed * Time.fixedDeltaTime;
        pRigid.MovePosition(pRigid.position + nextVec);
    }

    //프레임이 종료되기 전 실행되는 생명 주기 함수
    void LateUpdate()
    {
        pAnim.SetFloat("Speed",pInputVec.magnitude);
        if (pInputVec.x!=0)
        {
            // x축이 0보다 크면
            pSpriter.flipX = pInputVec.x < 0 ? true : false;
        }
    }
}
