using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic C# script to define the cursors used.
// This does not need to be its own script and can be absorbed into some other UIManager.cs, etc.

public class CursorSetting : MonoBehaviour
{
    // default cursor to use - initialize in Awake() or Start()
    public static Texture2D d_cursor;
    
    // "interactable" cursor - initialize in Awake() or Start()
    public static Texture2D i_cursor;

    // offset for the cursor's focus point (from top left corner)
    public static Vector2 hotspot = new Vector2(2f, 2f);

    //private Camera mainCam;

    // define the cursors from Resources Folder
    void Awake()
    {
        d_cursor = Resources.Load("default_cursor") as Texture2D;
        i_cursor = Resources.Load("interactive_cursor") as Texture2D;   
    }
    
    // set default cursor at start
    void Start()
    {
        //mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Cursor.SetCursor(d_cursor, hotspot, CursorMode.Auto);
    }

    void Update()
    {
        // MOVE THIS OBJECT (AND ITS ATTACHED COLLIDER) TO MOUSE POSITION IN WORLD SPACE
        // MAKE SURE CURSOR_MANAGER HAS A COLLIDER ATTACHED TO IT
        this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, 
            Input.mousePosition.y,
            11));
    }
}
