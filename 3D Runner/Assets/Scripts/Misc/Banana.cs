using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{
    public float rotateSpeed;
    float distBetweenPlayer;
    public float attractDist;
    public float speed;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerManager.bananaCount = PlayerPrefs.GetInt("amount");
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);      
        
        if (PlayerManager.activateMagnet)
        {
            Attracted();
        }
    }

    void Attracted()
    {
        distBetweenPlayer = Vector3.Distance(transform.position, player.transform.position); //calculate distance

        if (attractDist >= distBetweenPlayer)
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.bananaCount += 1;
            PlayerPrefs.SetInt("amount", PlayerManager.bananaCount);
            FindObjectOfType<AudioManager>().PlaySound("Collect banana");
            Destroy(gameObject);
        }
    }
}
