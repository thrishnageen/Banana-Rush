using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttackIndicator : MonoBehaviour
{
    SpriteRenderer sprite;
    public float chaseSpeed;
    public float rotateSpeed = 10f;
    public float timeToExpand;
    public float expandSpeed;
    public float blinkSpeed;
    bool startExpand = false;
    bool stopMoving = false;

    Vector3 minScale;
    public Vector3 maxScale;
    public GameObject player;
    public GameObject laser;

    public static bool restartTimer = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        transform.position = player.transform.position;
        minScale = transform.localScale;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * rotateSpeed);

        if (!startExpand)
        {
            startExpand = true;
            StartCoroutine(Expand());
        }

        if (!stopMoving)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position, chaseSpeed * BossVariable.injuredMultiplier * Time.deltaTime);
        }
        transform.position = new Vector3(transform.position.x, 0.5f, player.transform.position.z);

        //transform.position = Vector3.Lerp(transform.position, newPos, chaseSpeed * Time.deltaTime);
    }

    IEnumerator Expand()
    {
        float i = 0.0f;
        float rate = (1.0f / timeToExpand) * expandSpeed;

        //when object is not fully scaled to specified size
        while (i < 1.0f)
        {
            //how quick to lerp
            i += Time.deltaTime * rate * BossVariable.injuredMultiplier;
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, i); //fade in
            transform.localScale = Vector3.Lerp(minScale, maxScale, i);
            yield return null;
        }

        StartCoroutine(Blink());
    }


    IEnumerator Blink()
    {
        stopMoving = true;

        for (int x = 0; x < 3; x++)
        {
            sprite.enabled = false;
            FindObjectOfType<AudioManager>().PlaySound("Target Lock");
            yield return new WaitForSeconds(blinkSpeed / BossVariable.injuredMultiplier);
            sprite.enabled = true;
            yield return new WaitForSeconds(blinkSpeed / BossVariable.injuredMultiplier);
        }
        restartTimer = true;
        Instantiate(laser);
        FindObjectOfType<AudioManager>().PlaySound("Shoot");
        BossVariable.isShootingLaser = false;
        Destroy(gameObject);
    }
}
