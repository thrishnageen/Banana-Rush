using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowAxe : MonoBehaviour
{
    public GameObject axe;
    public GameObject axeUI;

    void Update()
    {
        if (!PlayerManager.hasWeapon)
            axeUI.SetActive(false);
        else
            axeUI.SetActive(true);
    }

    public void ThrowingAxe()
    {
        if (!Controller.isDead)
        {
            FindObjectOfType<AudioManager>().PlaySound("Throw");
            Instantiate(axe);
            PlayerManager.hasWeapon = false;
        }
  
    }
}
