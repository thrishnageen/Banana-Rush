using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossCar : MonoBehaviour
{
    [HideInInspector]
    public CharacterController charControl;

    [Header("Move Stats")]
    public float initialSpeed;
    float speed;
    public float closingDistance;
    float distance;
    public float gravity;
    public float jumpVel;
    public Vector3 dir;


    public GameObject wave;
    public int maxHealth;
    public int health;
    public Text healthText;
    bool injuredTaunt = true;

    public Transform player;

    void Start()
    {
        charControl = GetComponent<CharacterController>();
        speed = initialSpeed;
        health = maxHealth;
    }

    void Update()
    {
        healthText.text = "Boss HP: " + health;

        charControl.Move(dir * Time.deltaTime);

        if (PlayerManager.gameStart)
        {
            if (!charControl.isGrounded)
            {
                dir.y += gravity * BossVariable.injuredMultiplier * Time.deltaTime; //gravity simulation
            } else
            {
                if (ChooseAttacks.stompAttack)
                {
                    FindObjectOfType<AudioManager>().PlaySound("Explode");
                    Instantiate(wave);
                    StartCoroutine(FindObjectOfType<ShakeCamera>().Shake(0.6f, Random.Range(.6f, 1f)));
                    ChooseAttacks.stompAttack = false;
                    ChooseAttacks.canTaunt = true;
                }

            }

            if (BossVariable.startTaunt)
                Appear();
        }
       
        ChasePlayer();

        if (health < maxHealth / 2)
        {
            if (injuredTaunt && ChooseAttacks.canTaunt)
            {
                BossVariable.injuredMultiplier = 1.5f;
                BossVariable.taunt = true;
                ChooseAttacks.canTaunt = false;
                injuredTaunt = false;
            }
        }

        if (health <= 0)
        {
            PlayerManager.gameOver = true;
            PlayerManager.victory = true;
            FindObjectOfType<AudioManager>().PlaySound("Explode");
            Destroy(gameObject);
        }
       
    }

    void Appear()
    {
        if (charControl.isGrounded)
        {
            BossVariable.startTaunt = false;
            FindObjectOfType<AudioManager>().PlaySound("Explode");
            StartCoroutine(FindObjectOfType<ShakeCamera>().Shake(Random.Range(.2f, .5f), Random.Range(.9f, 1.5f)));
            BossVariable.taunt = true;
        }
    }

    public void ChasePlayer()
    {
        distance = Vector3.Distance(transform.position, player.transform.position); //calculate distance

        //if distance between boss and player is too much, boss runs faster
        if (distance > closingDistance)
        {
            speed = initialSpeed * 5;
        }
        else
        {
            speed = initialSpeed / 1.2f;
        }


        if (BossVariable.startChasing)
            dir.z = speed;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Obstacle")
        {
            Destroy(hit.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("Nom");
        }

        if (hit.transform.tag == "Thrown Axe")
        {
            Destroy(hit.gameObject);
            FindObjectOfType<AudioManager>().PlaySound("Hit Boss");
            health--;
        }
    }
}
