using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBananaCount : MonoBehaviour
{
    public Text bananaText;
    public Text healthText;

    void Update()
    {
        PlayerManager.bananaCount = PlayerPrefs.GetInt("amount");
        bananaText.text = "Banana: " + PlayerManager.bananaCount;
        if (PlayerPrefs.GetInt("Health") == 0)
            PlayerPrefs.SetInt("Health", 5);
        PlayerManager.playerHealth = PlayerPrefs.GetInt("Health");
        healthText.text = "x" + PlayerManager.playerHealth;
        bananaText.text = "Banana: " + PlayerManager.bananaCount;
    }
}
