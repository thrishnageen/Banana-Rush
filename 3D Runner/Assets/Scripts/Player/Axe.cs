using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public float rotateSpeed;


    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !PlayerManager.hasWeapon)
        {
            PlayerManager.hasWeapon = true;
            FindObjectOfType<AudioManager>().PlaySound("Obtain Axe");
            Destroy(gameObject);
        }
    }
}
