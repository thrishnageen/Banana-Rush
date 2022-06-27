using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateShield : MonoBehaviour
{
    public GameObject shield;
    public static bool isShieldActivated = false;
    void Update()
    {
        if (PlayerManager.shield == 0)
            gameObject.SetActive(false);

        if (isShieldActivated)
            gameObject.SetActive(false);
    }

    public void ShieldActivation()
    {
        if (!Controller.isDead)
        {
            FindObjectOfType<AudioManager>().PlaySound("Shield");
            shield.SetActive(true);
            isShieldActivated = true;
        }
    }
}
