using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{

    public int maxProgress = 100;
    public int progression;
    public Sprite[] sprites;
    private SpriteRenderer renderer;
   
	// Use this for initialization
	void Start ()
	{
        renderer = GetComponent<SpriteRenderer>();        
	}
	
	// Update is called once per frame
	void Update ()
	{
        Debug.Log(GetProgressionFrame());
        renderer.sprite = sprites[GetProgressionFrame()];

	}

    private int GetProgressionFrame()
    {
        int frame = progression * sprites.Length / maxProgress;
        if (frame >= sprites.Length) return sprites.Length - 1;
        if (frame <= 0) return 0;
        return frame;
    }
}
