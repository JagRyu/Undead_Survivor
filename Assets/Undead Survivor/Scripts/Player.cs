using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 pInputVec;
    public float pSpeed;
    Rigidbody2D pRigid;


    //√ ±‚»≠
    void Awake()
    {
        pRigid = GetComponent<Rigidbody2D>();
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
}
