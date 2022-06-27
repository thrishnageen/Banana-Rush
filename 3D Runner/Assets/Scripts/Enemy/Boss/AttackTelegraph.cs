using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTelegraph : MonoBehaviour
{
    [Header("Shoot Laser Telegraph")]
    Vector3 minScale;
    public Vector3 maxScale;
    public float expandSpeed;
    public float duration;
    bool animationEnded = false;

    void Start()
    {
        minScale = transform.localScale;
    }

    void Update()
    {
        if (ChooseAttacks.attacking)
        {
            StartCoroutine(Expand(minScale, maxScale, duration));
            
            if (!BossVariable.isShootingLaser)
            {
                StartCoroutine(FinishShoot());
            }

        }
       
    }


    IEnumerator FinishShoot()
    {
        yield return Expand(maxScale, minScale, duration);
        ChooseAttacks.attacking = false;
    }
    IEnumerator Shoot()
    {
        yield return Expand(minScale, maxScale, duration);


    }

    IEnumerator Recharge()
    {
        yield return Expand(maxScale, minScale, duration);

    }

    public IEnumerator Expand(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * expandSpeed;

        //when object is not fully scaled to specified size
        while (i < 1.0f)
        {
            //how quick to lerp
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(a, b, i);
            yield return null;
        }
    }
}
