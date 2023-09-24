using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSpriteManager : MonoBehaviour
{
    public Image img;
    public Sprite sprite1;
    public Sprite sprite2;

    public float timeBetweenFrames = 0.1f;

    float t = 0;

    private void Update()
    {
        t += Time.deltaTime;
        
        if(t >= timeBetweenFrames)
        {
            img.sprite = img.sprite == sprite1 ? sprite2 : sprite1;
            t = 0;
        }
    }
}
