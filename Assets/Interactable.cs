using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basic script to add to any interactable objects in the scene

public class Interactable : MonoBehaviour
{
    public bool interacted = false;     // true if object has been interacted with
    public bool selected = false;       // true if object is currently being interacted with (cam zoom?)
    
    private Sprite defaultSprite;       // the original sprite to be rendered

    // can possibly turn this into an array if more sprite "stages" are desired
    [SerializeField] private Sprite interactedSprite;   // new sprite to render after being interacted with

    private CameraManager mainCam;

    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = this.GetComponent<SpriteRenderer>().sprite;
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    // LEFT CLICK ON AN OBJECT - swap its "interacted" value
    // easy way to detect left click on a sprite
    void OnMouseDown(){

        //this.interacted = !this.interacted;     //swap interacted value
        this.interacted = true;
        this.selected = true;
        mainCam.cameraZoom(this.transform.position);
        
        Debug.Log("Clicked on " + this.name);

        // OPTIONAL: change the object sprite if a new one is assigned
        if(this.interactedSprite)
            this.GetComponent<SpriteRenderer>().sprite = interactedSprite;
        
    }

    // RIGHT CLICK ON AN OBJECT - stop focus on object (reset camera)
    // detecting right click on an object using a different approach
    void OnMouseOver () {
        if (Input.GetMouseButtonDown(1)) 
        {
            //Debug.Log(this.name + " is " + (interacted ? "selected" : "not selected") );
            this.selected = false;
            mainCam.cameraReset();
        }
    }
    
    // change cursor on entering a sprite
    void OnMouseEnter()
    {
        Cursor.SetCursor(CursorSetting.i_cursor, CursorSetting.hotspot, CursorMode.Auto);
    }

    // change cursor back to default on leaving a sprite
    void OnMouseExit()
    {
        Cursor.SetCursor(CursorSetting.d_cursor, CursorSetting.hotspot, CursorMode.Auto);
    }
}
