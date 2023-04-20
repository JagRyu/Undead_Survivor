using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int wId;
    public int wPrefabId;
    public float wDamage;
    public int wCount;
    public float wSpeed;


    float timer;
    Player player;


    void Awake()
    {
        player = GetComponentInParent<Player>();
    }
    void Start()
    {
        Init();
    }
    void Update()
    {
        switch (wId)
        {
            case 0:
                transform.Rotate(Vector3.forward * wSpeed * Time.deltaTime);
                break;
            case 1:
                timer += Time.deltaTime;
                if(timer> wSpeed)
                {
                    timer = 0;
                    Fire();
                }
                break;

            default:
                break;

        }
        
    }
    public void LevelUp(float damage, int count)
    {
        this.wDamage = damage;
        this.wCount += count;
        if(wId == 0)
        {
            Batch();
        }


    }
    public void Init()
    {
        switch (wId) 
        {
            case 0:
                wSpeed = -150; // ���̳ʽ��� �ð�������� ����.
                Batch();
                break;
            case 1:
                wSpeed = 0.3f;

                break;
            default:
                break;
        }
    }
    void Batch()
    {
        //������ ���⸦ ��ġ�ϴ� �Լ�
        for(int index = 0; index< wCount; index++)
        {
            Transform bullet ;
            if(index< transform.childCount)
            {
                //�ڽĿ� �ִ°� ��������
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instace.pool.Get(wPrefabId).transform;
                bullet.parent = transform;
            }
            //������ object�� weapon0 object�� �ڽ����� �־��ٲ���

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / wCount;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.2f, Space.World);

            bullet.GetComponent<Bullet>().Init(wDamage, -1, Vector3.zero); //-1 is Infinity Per.


        }
    }
    void Fire()
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 targetDir = targetPos - transform.position;
        targetDir = targetDir.normalized;

        Transform bullet = GameManager.instace.pool.Get(wPrefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up,targetDir);

        bullet.GetComponent<Bullet>().Init(wDamage, wCount, targetDir); //-1 is Infinity Per.

    }
}