using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameStart = false, gameOver, victory = false, hasWeapon = false, reset = false, activateMagnet = false, canTakeDamage = true;
    public static int bananaCount, playerHealth = 1, shield = 0, magnet = 0;
    public GameObject startText, gameOverUI;
    public Text bananaText, healthText;
    public bool isMainMenu = false;

    void Awake()
    {
        gameOver = false;
        gameStart = false;
        Time.timeScale = 1;
        shield = PlayerPrefs.GetInt("Shield");
        if (PlayerPrefs.GetInt("Health") == 0)
            PlayerPrefs.SetInt("Health", 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMainMenu)
        {
            if (Swipe.tap)
            {
                gameStart = true;
                Destroy(startText);
            }

            bananaText.text = "Banana: " + bananaCount;

            if (gameOver)
            {
                //pause
                Time.timeScale = 0;
                gameOverUI.SetActive(true);
            }
        }

        if (reset)
        {
            reset = false;
            Reset();
        }

        playerHealth = PlayerPrefs.GetInt("Health");
        shield = PlayerPrefs.GetInt("Shield");
    }

    public void Reset()
    {
        gameStart = gameOver = victory = hasWeapon = activateMagnet = false;
        canTakeDamage = true;
        Controller.isDead = false;
        BossVariable.injuredMultiplier = 1f;
        BossVariable.changeColor = BossVariable.startChasing = BossVariable.taunt = BossVariable.isShootingLaser = ChooseAttacks.stompAttack = false;
        BossVariable.startTaunt = true;

    }
}
