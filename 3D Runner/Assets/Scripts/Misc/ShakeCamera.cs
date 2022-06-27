using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        //0% finished
        float i = 0f;
        float rate = (1.0f / duration);

        while (i < duration)
        {
            i += Time.deltaTime;
            float x = Random.Range(-1f, 1f) * magnitude; //shakes harder depending on set magnitude
            float y = Random.Range(-1f, 1f) * magnitude;

            //transform.localPosition = new Vector3(x, originalPos.y, originalPos.z);

            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(x, originalPos.y, transform.localPosition.z), magnitude);

            yield return null;
        }

        transform.localPosition = new Vector3(originalPos.x, originalPos.y, transform.localPosition.z);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
