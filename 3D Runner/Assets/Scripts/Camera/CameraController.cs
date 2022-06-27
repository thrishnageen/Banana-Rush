using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    public float lerpSpeed;

    void Start()
    {
        offset = transform.position - player.position;
    }

    //follow along player
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, offset.z + player.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, lerpSpeed * Time.deltaTime); 
    }
}
