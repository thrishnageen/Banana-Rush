using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject victoryText;
    public GameObject failureText;

    void Update()
    {
        if (PlayerManager.gameOver && PlayerManager.victory)
        {
            victoryText.SetActive(true);
        } else if (PlayerManager.gameOver && !PlayerManager.victory)
        {
            failureText.SetActive(true);
        }
    }
}
