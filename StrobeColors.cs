using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrobeColors : MonoBehaviour
{
    Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = this.GetComponent<Renderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float hue = Mathf.PingPong(Time.time, 1f);
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);

        renderer.material.color = rainbowColor;
    }
}
