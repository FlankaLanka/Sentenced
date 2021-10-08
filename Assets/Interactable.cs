using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

// basic script to add to any interactable objects in the scene
public class Interactable : MonoBehaviour
{
    public bool interacted = false;     // true if object has been interacted with

    public Flowchart fc;
    private Sprite defaultSprite;       // the original sprite to be rendered

    // can possibly turn this into an array if more sprite "stages" are desired
    [SerializeField] private Sprite interactedSprite;   // new sprite to render after being interacted with

    InteractManager im;


    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = this.GetComponent<SpriteRenderer>().sprite;
        im = GameObject.Find("InteractManager").GetComponent<InteractManager>();
        fc = GameObject.Find("Flowchart").GetComponent<Flowchart>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // LEFT CLICK ON AN OBJECT - swap its "interacted" value
    // easy way to detect left click on a sprite
    void OnMouseDown(){

        //Debug.Log("Clicked on " + this.name);

        // select this object
        im.Select(this.gameObject);

        //change interacted value
        //this.interacted = !this.interacted;     // swap           
        this.interacted = true;                   // change to true (one time only)

        // OPTIONAL: change the object sprite if a new one is assigned
        if(this.interactedSprite)
            this.GetComponent<SpriteRenderer>().sprite = interactedSprite;
            
    }

    // RIGHT CLICK ON AN OBJECT - stop focus on object (reset camera)
    // detecting right click on an object using a different approach
    void OnMouseOver () {
        if (Input.GetMouseButtonDown(1)) 
        {
            if (!fc.GetBooleanVariable("locked")){
                // deselect this object
                im.Deselect();
            }
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
