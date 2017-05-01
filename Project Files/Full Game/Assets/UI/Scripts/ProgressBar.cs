using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    public int maxProgress = 100;
    public int progression;
    public Sprite[] sprites;
    private Image image;
	private SpriteRenderer spriteRenderer;
	public ProgressBarRenderType renderType;
	public 
   
	// Use this for initialization
	void Start ()
	{
        image = GetComponent<Image>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Debug.Log(GetProgressionFrame());
		if (renderType == ProgressBarRenderType.Image)
			image.sprite = sprites[GetProgressionFrame()];
		else
			spriteRenderer.sprite = sprites[GetProgressionFrame()];
		
	}

    private int GetProgressionFrame()
    {
        int frame = progression * sprites.Length / maxProgress;
        if (frame >= sprites.Length) return sprites.Length - 1;
        if (frame <= 0) return 0;
        return frame;
    }

	public enum ProgressBarRenderType
	{
		Image,
		SpriteRenderer
	}
}
