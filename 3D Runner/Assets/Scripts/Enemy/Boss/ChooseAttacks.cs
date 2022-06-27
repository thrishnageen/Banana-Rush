using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAttacks : MonoBehaviour
{
    [Header("Attack Interval")]
    public float minTime, maxTime;
    float timeInterval;
    bool spawned = false;

    [Header("Laser")]
    public GameObject laserIndicator;
    public static bool attacking = true;

    int option = 1;
    bool startStomp = true, pickOption = true;
    public static bool stompAttack = false, canTaunt = true;

    void Start()
    {
        timeInterval = Random.Range(minTime, maxTime);
    }


    void Update()
    {
        if (BossVariable.startChasing)
        {
            if (timeInterval > 0)
            {
                timeInterval -= Time.deltaTime;
            }
            else
            {
                if (pickOption)
                {
                    pickOption = false;
                    option = Random.Range(1, 3);
                }

                switch (option)
                {
                    case 1:
                        ShootLaser();
                        break;

                    case 2:
                        startStomp = true;
                        pickOption = true;
                        Stomping();
                        break;
                }

            }

        }

    }

    void ShootLaser()
    {
        if (!spawned)
        {
            attacking = true;
            BossVariable.isShootingLaser = true;
            Instantiate(laserIndicator);
            spawned = true;
            canTaunt = false;
        }

        if (LaserAttackIndicator.restartTimer)
        {
            pickOption = true;
            spawned = false;
            timeInterval = Random.Range(minTime, maxTime) / BossVariable.injuredMultiplier;
            LaserAttackIndicator.restartTimer = false;
            canTaunt = true;
        }

    }

    void Stomping()
    {
        if (startStomp)
        {
            GetComponent<BossCar>().dir.y = 30f;
            startStomp = false;
            stompAttack = true;
            timeInterval = 3f / BossVariable.injuredMultiplier;
            canTaunt = false;
        }
    }


    //IEnumerator Stomp()
    //{
    //    if (startStomp)
    //    {
    //        Debug.Log("Jump");
    //        GetComponent<BossCar>().dir.y = 30f;
    //        startStomp = false;
    //        stompAttack = true;
    //    }


    //    yield return new WaitForSeconds(.5f);
       
    //    if (GetComponent<BossCar>().charControl.isGrounded)
    //    {
    //        if (stompAttack)
    //        {
    //            pickOption = true;
    //            stompAttack = false;
    //            Debug.Log("Grounded");
    //            timeInterval = Random.Range(minTime, maxTime) / BossVariable.injuredMultiplier;
    //            startStomp = true;
    //        }
            
    //    }

    //    yield return null;

    //}

}
