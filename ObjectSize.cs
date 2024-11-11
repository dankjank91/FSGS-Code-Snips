using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSize : MonoBehaviour
{
    Vector2 centerScreen = new Vector2(0,0);
    float distance;
    float minSize = .05f;
    float maxSize = 3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSize();
    }

    void CalculateSize()
    {        
        distance = Vector2.Distance(this.transform.position,centerScreen);
        if(distance>=maxSize)
        {
            distance=maxSize;
        }
        else if(distance<=minSize)
        {
            distance = minSize;
        }

        transform.localScale = new Vector3(distance,distance,0);
    }
}
