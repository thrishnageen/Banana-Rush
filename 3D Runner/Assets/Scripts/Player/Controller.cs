using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(CharacterController))]
public class Controller : MonoBehaviour
{
    CharacterController charControl;
    public GameObject shieldObject;
    public GameObject player;
    Vector3 dir;

    public int health = 1;
    public Text healthText;
    public static bool isDead = false;

    [Header("Move Stats")]
    public float moveSpeed;
    public float maxMoveSpeed;
    public float moveToLaneSpeed = 80;
    public float slideTime;
    bool isSliding = false;
    public static int lanePos = 1; //0 = left, 1 = mid, 2 = right lane
    public float laneDist = 4; //distance between each lanes

    [Header("Jump Stats")]
    public float jumpVel;
    public float jumpDownVel;
    public float gravity;

    public Animator anim;
    MeshRenderer baseSprite;

    void Start()
    {
        charControl = GetComponent<CharacterController>();
        baseSprite = GetComponent<MeshRenderer>();
        baseSprite.enabled = false;
        PlayerManager.playerHealth = PlayerPrefs.GetInt("Health");

        health = PlayerManager.playerHealth;

    }

    void Update()
    {
        healthText.text = "Health: " + health;

        //game has not started
        if (!PlayerManager.gameStart)
            return;

        if (!isDead)
        {
            anim.SetBool("isGameStarted", true);

            //game faster

            if (moveSpeed <= maxMoveSpeed)
            {
                //moveSpeed += 0.15f * Time.deltaTime;
            }

            dir.z = moveSpeed;

            Movement();

            //jump
            if (charControl.isGrounded)
            {
                anim.SetBool("isGrounded", true);
                Jump();
            }
            else
            {
                anim.SetBool("isGrounded", false);
                dir.y += gravity * Time.deltaTime; //gravity simulation
            }

            if (Swipe.swipeDown && !isSliding && charControl.isGrounded)
            {
                StartCoroutine(Slide());
            }

            if (Swipe.swipeDown && !charControl.isGrounded)
            {
                dir.y = -jumpDownVel;
            }

        }

        if (health <= 0)
        {
            anim.SetTrigger("isDead");
            StartCoroutine(Dying());

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PlayerManager.gameStart)
            return;

        if (!isDead) 
          charControl.Move(dir * Time.deltaTime);

    }

    void Movement()
    {
        if (Swipe.swipeLeft)
        {
            lanePos++;
            if (lanePos == 3)
                lanePos = 2;
        }

        if (Swipe.swipeRight)
        {
            lanePos--;
            if (lanePos == -1)
                lanePos = 0;
        }

        Vector3 newPos = transform.position.z * transform.forward + transform.position.y * transform.up;

        if (lanePos == 0)
        {
            newPos += Vector3.left * laneDist;
        }
        else if (lanePos == 2)
        {
            newPos += Vector3.right * laneDist;
        } 

        transform.position = Vector3.Lerp(transform.position, newPos, moveToLaneSpeed * Time.deltaTime);
    }

    void Jump()
    {
        if (Swipe.swipeUp)
        {
            anim.SetBool("isSliding", false);
            dir.y = jumpVel;
        }
    }

    IEnumerator Slide()
    {
        isSliding = true;
        anim.SetBool("isSliding", true);
        charControl.center = new Vector3(0, -0.5f, 0);
        charControl.height = 1;
        yield return new WaitForSeconds(slideTime);
        isSliding = false;
        charControl.center = new Vector3(0, 0, 0);
        charControl.height = 2;
        anim.SetBool("isSliding", false);
    }

    public IEnumerator IFrame()
    {
        for (int x = 0; x < 10; x++)
        {
            player.GetComponent<SkinnedMeshRenderer>().enabled = false;
            yield return new WaitForSeconds(.1f);
            player.GetComponent<SkinnedMeshRenderer>().enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        PlayerManager.canTakeDamage = true;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!isDead)
        {
            if (hit.transform.tag == "Obstacle")
            {
                if (PlayerManager.canTakeDamage)
                {
                    PlayerManager.canTakeDamage = false;
                    StartCoroutine(IFrame());
                   
                    if (!ActivateShield.isShieldActivated)
                    {
                        health--;
                        FindObjectOfType<AudioManager>().PlaySound("Scream");
                    }
                    else
                        FindObjectOfType<AudioManager>().PlaySound("Nope");

                }

                Destroy(hit.gameObject);
            }
        }
    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Laser")
        {
            if (PlayerManager.canTakeDamage)
            {
                PlayerManager.canTakeDamage = false;
                StartCoroutine(IFrame());

                if (!ActivateShield.isShieldActivated)
                {
                    FindObjectOfType<AudioManager>().PlaySound("Scream");
                    health--;
                }
                else
                    FindObjectOfType<AudioManager>().PlaySound("Nope");

                Destroy(other.gameObject);
            }

        }

    }

    IEnumerator Dying()
    {
        BossVariable.injuredMultiplier = 1f;
        isDead = true;
        moveSpeed = 0;
        yield return new WaitForSeconds(2f);
        isDead = false;
        PlayerManager.gameOver = true;
    }

}
