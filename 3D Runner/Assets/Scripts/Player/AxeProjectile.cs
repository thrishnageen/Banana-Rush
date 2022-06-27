using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    public float rotateSpeed;
    public float speed;

    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

        transform.position += new Vector3(0f, 0f, speed * Time.deltaTime);
    }
}
