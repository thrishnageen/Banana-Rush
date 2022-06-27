using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float duration;
    bool flag = true;
    MeshRenderer sprite;

    void Start()
    {
        sprite = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (duration > 0)
        {
            ActivateShield.isShieldActivated = true;
            duration -= Time.deltaTime;
        }

        if (duration <= 0 && flag)
        {
            flag = false;
            StartCoroutine(Blink());
        }
    }

    IEnumerator Blink()
    {
        for (int x = 0; x < 3; x++)
        {
            sprite.enabled = false;
            FindObjectOfType<AudioManager>().PlaySound("Poot");
            yield return new WaitForSeconds(.5f);
            sprite.enabled = true;
            yield return new WaitForSeconds(.5f);
        }

        FindObjectOfType<AudioManager>().PlaySound("Expire");
        ActivateShield.isShieldActivated = false;
        gameObject.SetActive(false);
    }

}
