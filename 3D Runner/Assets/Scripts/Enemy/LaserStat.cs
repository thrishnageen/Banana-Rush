using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserStat : MonoBehaviour
{
    public float speed;
    float distance;
    public float despawnLength;
    public float height = 1;
    public GameObject player;
    public GameObject bossCar;
    public bool isWave = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bossCar = GameObject.FindGameObjectWithTag("Boss");
        if (!isWave)
            transform.position = new Vector3(GameObject.FindGameObjectWithTag("Indicator").transform.position.x, height, bossCar.transform.position.z + 35f);
        else
            transform.position = new Vector3(1.2f, height, bossCar.transform.position.z + 35f);

    }

    void Update()
    {
        //dir.z = speed;
        if (!isWave)
            transform.position += new Vector3(0f, 0f, speed * BossVariable.injuredMultiplier * Time.deltaTime);
        else
            transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);

        distance = Vector3.Distance(transform.position, player.transform.position); //calculate distance

        if (distance > despawnLength)
            Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.transform.tag == "Player")
    //    {
    //        if (PlayerManager.canTakeDamage)
    //        {
    //            PlayerManager.canTakeDamage = false;
    //            StartCoroutine(other.gameObject.GetComponent<Controller>().IFrame());

    //            if (!ActivateShield.isShieldActivated)
    //            {
    //                FindObjectOfType<AudioManager>().PlaySound("Scream");
    //                other.gameObject.GetComponent<Controller>().health--;
    //            }
    //            else
    //                FindObjectOfType<AudioManager>().PlaySound("Nope");

    //            Destroy(gameObject);
    //        }

    //    }

    //}
}
