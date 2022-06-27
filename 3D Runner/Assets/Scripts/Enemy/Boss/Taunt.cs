using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : MonoBehaviour
{
    Vector3 minScale;
    public Vector3 maxScale;
    public float speed;
    public float duration = 0.5f;

    void Start()
    {
        minScale = transform.localScale;
    }

    void Update()
    {
        if (BossVariable.taunt)
        {
            BossVariable.taunt = false;
            StartCoroutine(Taunting());
        }
    }

    IEnumerator Taunting()
    {
        BossVariable.changeColor = true;
        yield return new WaitForSeconds(0.5f);
        //honk twice
        FindObjectOfType<AudioManager>().PlaySound("Honk");
        if (BossVariable.injuredMultiplier <= 1)
            StartCoroutine(FindObjectOfType<ShakeCamera>().Shake(1.5f, Random.Range(.8f, 1.3f)));
        for (int x = 0; x < 2; x++)
        {
            yield return RepeatLerp(minScale, maxScale, duration);
            yield return RepeatLerp(maxScale, minScale, duration * 2);
        }
        yield return new WaitForSeconds(0.5f);
        BossVariable.startChasing = true;
    }

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time)
    {
        float i = 0.0f;
        float rate = (1.0f / time) * speed;
     
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
