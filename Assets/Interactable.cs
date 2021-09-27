using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basic script to add to any interactable objects in the scene

public class Interactable : MonoBehaviour
{
    public bool interacted = false;

    // Start is called before the first frame update
    void Start()
    {

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
        
        Debug.Log("Clicked on " + this.name);
    }

    // RIGHT CLICK ON AN OBJECT - debug.log the status of this object
    // detecting right click on an object using a different approach
    void OnMouseOver () {
        if (Input.GetMouseButtonDown(1)) 
        {
            Debug.Log(this.name + " is " + (interacted ? "selected" : "not selected") );
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
