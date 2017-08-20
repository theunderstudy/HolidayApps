using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuDog : MonoBehaviour
{
    private Image image;
    public Sprite[] Sprites;
    private int frameIndex=0;
    public float timeBetweenFrames=0.2f;
    private float CurrentTime=0;
	// Use this for initialization
	
	
	void Update ()
    {
        if (CurrentTime <timeBetweenFrames )
        {
            CurrentTime += Time.deltaTime;
        }
        else
        {
            CurrentTime = 0;
            //next frame
            frameIndex++;
            if (frameIndex == Sprites.Length)
            {
                frameIndex = 0;
            }
            if (!image)            
                //assign image if we haven't
                image = GetComponent<Image>();
               
            
            image.sprite = Sprites[frameIndex];
        }
	}
}
