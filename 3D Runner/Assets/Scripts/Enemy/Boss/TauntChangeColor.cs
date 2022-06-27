using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TauntChangeColor : MonoBehaviour
{
    //change color
    MeshRenderer mesh;
    Color32 originalColor;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        originalColor = mesh.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (BossVariable.changeColor)
        {
            //turn red
            StartCoroutine(ChangeColor());
        }

    }

    IEnumerator ChangeColor()
    {
        mesh.material.color = Color32.Lerp(mesh.material.color, Color.red, .01f);

        yield return new WaitForSeconds(1.2f);

        mesh.material.color = Color32.Lerp(mesh.material.color, Color.white, .09f);
    }
}
