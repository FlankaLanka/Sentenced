using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

// Class to manage Interactable Objects, Camera Zoom, and Fungus Text

//////////////////////////////////////////////////////////////////////////////////////////////////////
// ------------------------------------------ IMPORTANT: ------------------------------------------ //
// the fungus "locked" variable is important in controlling what/when player can click              //
// -- if (locked == false), the player cannot click on any objects                                  //
// -- ideally, set locked to false at the start of the inspection + once all text has been said     //
// -- set locked to true once text is displayed or before the inspection phase                      //    
//                                                                                                  //
// fungus also needs an objName string variable -- unsure how things are being handled currently,   //
// but this is how i've implemented it so far.                                                      //
//                                                                                                  //    
// Interact Manager uses a large collider that covers the entire screen for OnMouseDown. Make sure  //
// that InteractManager's **z value in the scene is greater than all interactable objects** so that //
// you can still click on those other objects.                                                      //
//                                                                                                  //
// Interact Manager needs an *AudioSource* attached to handle select/deselect sfx                   //
// ------------------------------------------------------------------------------------------------ //
//////////////////////////////////////////////////////////////////////////////////////////////////////

// TLDR: 
// (Fungus) add a "locked" boolean variable and(???) "objName" string variable
// ** call InteractManager.Deselect() in fungus after finished "saying" text ** 
// (GameObject) add a giant collider that covers the whole screen
// (GameObject) make sure z value in the scene is larger than all interactable objects
// (GameObject) add an audio source (don't need to put any audio sources)

public class InteractManager : MonoBehaviour
{
    public Flowchart fc;
    
    private CameraMovement mainCam;
    private AudioClip selectSFX;
    private AudioClip deselectSFX;

    //public static bool selected;
    public string selected;
    public List<Interactable> interactableObjects;

    public bool hovering;  // true if player is hovering over an interactable object
    public bool shifting;

    public Vector3 prevMousePos;

    // UI arrows to indicate if player can move scene
    public GameObject l_arrow;
    public GameObject r_arrow;
    public GameObject t_arrow;
    public GameObject b_arrow;

    // Start is called before the first frame update
    void Start()
    {
        selected = "";

        fc = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        //fc = GameObject.FindObjectOfType<Flowchart>();
        mainCam = GameObject.FindObjectOfType<CameraMovement>();
        
        // getting SFX from resources 
        // MAKE SURE FILE IS IN RESOURCES FOLDER!!!!!!
        selectSFX = Resources.Load("select") as AudioClip;
        deselectSFX = Resources.Load("deselect") as AudioClip;

        hovering = false;
        shifting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (selected == ""){
            // dealing with UI arrows for scene scroll
            l_arrow.SetActive(mainCam.transform.position.x - mainCam.minx > 0.05f);
            r_arrow.SetActive(mainCam.transform.position.x - mainCam.maxx < -0.05f);
            t_arrow.SetActive(mainCam.transform.position.y - mainCam.miny > 0.05f);
            b_arrow.SetActive(mainCam.transform.position.y - mainCam.maxy < -0.05f);
        }
        else{
            l_arrow.SetActive(false);
            r_arrow.SetActive(false);
            t_arrow.SetActive(false);
            b_arrow.SetActive(false);
        }
    }

    public bool Select(GameObject obj)
    {
        // only execute this code if object is not already selected
        if (selected != obj.name && !fc.GetBooleanVariable("locked"))
        {
            //selected = true;
            selected = obj.name;

            mainCam.cameraZoom(obj.transform.position);

            // "lock" the scene upon getting clicked on
            fc.SetStringVariable("objName", obj.name);
            
            this.GetComponent<AudioSource>().PlayOneShot(selectSFX);
        }
        else
        {
            Debug.Log("Calling Select() with a selected object");
            return false;
        }
        
        return true;
    }

    public void Deselect()
    {
        // only deselect if clicked on the selected object
        if (selected != "")
        {
            //selected = false;
            foreach (Interactable i in interactableObjects)
            {
                if (i.gameObject.name != selected)
                    i.HideOutline();
            }
            
            selected = "";
            mainCam.cameraReset();
            
            // change the cursor to normal and change hovering
            Cursor.SetCursor(CursorSetting.d_cursor, CursorSetting.hotspot, CursorMode.Auto);
            hovering = false;
            

            // "unlock" the scene upon getting deselected
            fc.SetStringVariable("objName", "");
            this.GetComponent<AudioSource>().PlayOneShot(deselectSFX);
            
        }
        else
        {
            Debug.Log("Calling Deselect() with no selected object");
        }
    }

    public void CheckAllInteractableObjectStatus() 
    {
        // check for all interactable objects in the scene
        //interactableObjects = Object.FindObjectsOfType<Interactable>();
        Debug.Log("Checking Interactables Status:");
        
        foreach (Interactable i in interactableObjects)
        {
            Debug.Log(i.name + " interaction: " + i.interacted );
        }
    }

    void OnMouseDown()
    {
        // 3 preconditions to dragging camera:
        // cannot click on an interactable object, scene cannot be locked, and object cannot be selected
        if (!hovering && !fc.GetBooleanVariable("locked") && selected == "")
        {
            shifting = true;
            prevMousePos = Input.mousePosition; // store 
            mainCam.cameraLocked = true;
        }
    }

    void OnMouseUp()
    {
        // releasing mouse from drag
        if (shifting && !hovering && !fc.GetBooleanVariable("locked") && selected == "")
        {
            shifting = false;
            mainCam.init_camPos = new Vector3(mainCam.transform.position.x,
                                              mainCam.transform.position.y,
                                              -10 );
            mainCam.target = mainCam.init_camPos;
            mainCam.cameraLocked = false;
        }
    }

    void OnMouseDrag()
    {   
        if (shifting)
        {
            // change transform? 
            Vector3 del = (Input.mousePosition - prevMousePos)/100;
            Vector3 newPos = mainCam.transform.position - (Vector3)del;
            mainCam.transform.position = new Vector3(
                    Mathf.Min(Mathf.Max(newPos.x, mainCam.minx), mainCam.maxx),
                    Mathf.Min(Mathf.Max(newPos.y, mainCam.miny - (newPos.z)), mainCam.maxy),
                    newPos.z
            );
            
            prevMousePos = Input.mousePosition;
        }
    }
}
