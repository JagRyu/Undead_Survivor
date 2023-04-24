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
        player = GameManager.instace.player;
    }
    
    void Update()
    {
        if (!GameManager.instace.isLive)
            return;

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
        player.BroadcastMessage("ApplyGear",SendMessageOptions.DontRequireReceiver);
    }
    public void Init(ItemData data)
    {
        //Basic Set
        name = "Weapon " + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        //Property Set
        wId = data.itemId;
        wDamage = data.baseDamage;
        wCount = data.baseCount;

        for (int index = 0; index < GameManager.instace.pool.prefabs.Length; index++)
        {
            if(data.projectile == GameManager.instace.pool.prefabs[index])
            {
                wPrefabId = index;
                break;
            }
        }

        switch (wId) 
        {
            case 0:
                wSpeed = -150; // 마이너스면 시계방향으로 돈다.
                Batch();
                break;
            case 1:
                wSpeed = 0.3f;

                break;
            default:
                break;
        }
        // Hand Set
        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);


        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);

    }
    void Batch()
    {
        //생성된 무기를 배치하는 함수
        for(int index = 0; index< wCount; index++)
        {
            Transform bullet ;
            if(index< transform.childCount)
            {
                //자식에 있는걸 가져오기
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instace.pool.Get(wPrefabId).transform;
                bullet.parent = transform;
            }
            //생성된 object를 weapon0 object에 자식으로 넣어줄꺼임

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
