using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shopping : MonoBehaviour
{
    public GameObject shopUI;

    public void ShowShopUI()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        shopUI.SetActive(true);
    }

    public void CloseShopUI()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        shopUI.SetActive(false);
    }

    public void BuyHealth()
    {
        if (PlayerManager.bananaCount >= 30 && PlayerManager.playerHealth < 10)
        {
            FindObjectOfType<AudioManager>().PlaySound("HECK YEAH");
            PlayerManager.bananaCount -= 30;
            PlayerPrefs.SetInt("amount", PlayerManager.bananaCount);
            PlayerManager.playerHealth += 1;
            PlayerPrefs.SetInt("Health", PlayerManager.playerHealth);
        } else
        {
            FindObjectOfType<AudioManager>().PlaySound("Nope");
        }
    }

    public void BuyShield()
    {
        if (PlayerManager.bananaCount >= 40 && PlayerManager.shield < 1)
        {
            FindObjectOfType<AudioManager>().PlaySound("HECK YEAH");
            PlayerManager.bananaCount -= 40;
            PlayerPrefs.SetInt("amount", PlayerManager.bananaCount);
            PlayerManager.shield += 1;
            PlayerPrefs.SetInt("Shield", PlayerManager.shield);
        } else
        {
            FindObjectOfType<AudioManager>().PlaySound("Nope");
        }
    }

    public void BuyMagnet()
    {
        if (PlayerManager.bananaCount >= 40 && PlayerManager.magnet < 1)
        {
            FindObjectOfType<AudioManager>().PlaySound("HECK YEAH");
            PlayerManager.bananaCount -= 40;
            PlayerPrefs.SetInt("amount", PlayerManager.bananaCount);
            PlayerManager.magnet += 1;
            PlayerPrefs.SetInt("Magnet", PlayerManager.magnet);
        }
        else
        {
            FindObjectOfType<AudioManager>().PlaySound("Nope");
        }
    }

}
