using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{

    public void Replay()
    {
        PlayerManager.reset = true;
        FindObjectOfType<AudioManager>().PlaySound("Click");
        SceneManager.LoadScene("Game");
    }

    public void ReturnToMenu()
    {
        PlayerManager.reset = true;
        FindObjectOfType<AudioManager>().PlaySound("Click");
        SceneManager.LoadScene("MainMenu");
    }

    public void DeleteSave()
    {
        FindObjectOfType<AudioManager>().PlaySound("No");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void Quit()
    {
        FindObjectOfType<AudioManager>().PlaySound("Click");
        Application.Quit();
    }

  
    
}
