using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMagnet : MonoBehaviour
{
    public float duration;
    public GameObject magnetUI;
    bool hasActivatedManget;

    // Update is called once per frame
    void Update()
    {
        if ((PlayerManager.activateMagnet && hasActivatedManget) || PlayerManager.magnet == 0)
            magnetUI.SetActive(false);


        if (PlayerManager.activateMagnet)
        {
            if (duration > 0)
                duration -= Time.deltaTime;
        }

        if (duration <= 0)
            PlayerManager.activateMagnet = false;

    }

    public void MagnetActivation()
    {
        if (!Controller.isDead)
        {
            hasActivatedManget = true;
            FindObjectOfType<AudioManager>().PlaySound("Activate Magnet");
            PlayerManager.activateMagnet = true;
        }
    }


}
