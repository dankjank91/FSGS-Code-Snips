using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject starObject;    
    Vector2 centerScreen = new Vector2(0,0);  
    public List<GameObject> starsInstatiated; 
    int maxStars = 25;
    float timer; 
    float maxTime = .4f;  
    public Vector2 screenBounds {get;set;}
    public Camera mainCamera;

    void Start()    // Start runs once, at the start of the scene.
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));        
        mainCamera = FindObjectOfType<Camera>(CompareTag("MainCamera"));
    }

    void Update()
    {

        timer+=Time.deltaTime;
        if(timer>=maxTime&&starsInstatiated.Count<=maxStars)
        {
            CreateStar(); 
            timer = 0;
        }
        
        
    }

    void CreateStar()   
    {

        for (int i = 0; i < maxStars; i++)
        {
        float randomSize = Random.Range(0.01f,.06f);
        GameObject newStar = Instantiate(starObject);
        newStar.transform.position = centerScreen;
        starsInstatiated.Add(newStar);
        newStar.transform.localScale = new Vector3(randomSize,randomSize,0);
        
        }


    }

}
