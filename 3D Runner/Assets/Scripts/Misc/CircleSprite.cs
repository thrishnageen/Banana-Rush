using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleSprite : MonoBehaviour
{
    Image image;
    Color color;

    void Start()
    {
        image = GetComponent<Image>();
        color = image.color;
    }

    void Update()
    {
        if (PlayerManager.hasWeapon)
            image.color = color;
        else
            image.color = Color.black;
    }
}
