using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Whiteboard : MonoBehaviour
{

    private int textureSize = 2048;
    private int pensize = 10;
    private Texture2D texture;
    private Color[] colours;


    private bool touching, touchingLast;

    float posX, posY, lastX, lastY;

    // Use this for initialization
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        texture = new Texture2D(textureSize, textureSize);
        rend.material.mainTexture = texture;
    }

    void Update()
    {
        int x = (int)(posX * textureSize - (pensize / 2));
        int y = (int)(posY * textureSize - (pensize / 2));

        if(touchingLast)
        {
            texture.SetPixels(x, y, pensize, pensize, colours);

            for (float t = 0.01f; t < 1.00f; t += 0.01f)
            {
                int lerpX = (int)Mathf.Lerp(lastX, (float)x, t);
                int lerpY = (int)Mathf.Lerp(lastY, (float)y, t);
                texture.SetPixels(lerpX, lerpY, pensize, pensize, colours);
            }

            texture.Apply();
        }

        lastX = (float)x;
        lastY = (float)y;

        touchingLast = touching;
    }


    public void ToggleTouch(bool touchingBoard)
    {
        this.touching = touchingBoard;
    }

    public void SetTouchPosition(float x, float y)
    {
        posX = x;
        posY = y;
    }

    public void SetColour(Color color)
    {
        colours = Enumerable.Repeat<Color>(color, pensize * pensize).ToArray<Color>();
    }
}
