using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour {

    public Whiteboard _whiteboard;
    private RaycastHit touch;
    public Transform _tip;
    public float drawThresHold;
    private bool lastTouch;
    private Quaternion lastAngle;
    public Color penColor;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 tipPos = _tip.transform.position;

        if(Physics.Raycast(tipPos, transform.up ,out touch, drawThresHold))
        {
            if (touch.collider.tag == "Whiteboard")
            {
               // Debug.Log("Poop");
                this._whiteboard = touch.collider.gameObject.GetComponent<Whiteboard>();

                _whiteboard.SetColour(penColor);
                _whiteboard.SetTouchPosition(touch.textureCoord.x, touch.textureCoord.y);
                _whiteboard.ToggleTouch(true);



                if (!lastTouch)
                {
                    lastTouch = true;
                    lastAngle = this.transform.rotation;
                }
            }
            else
            {
                _whiteboard.ToggleTouch(false);
                lastTouch = false;
            }
        }
        if (lastTouch)
        {
            transform.rotation = lastAngle;
        }
	}
}
