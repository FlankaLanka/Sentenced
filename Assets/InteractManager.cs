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
// Interact Manager uses a large collider that covers the entire screen for OnMouseDown. Make sure  //
// that InteractManager's z value in the scene is greater than all interactable objects so that you //
// can still click on those other objects.                                                          //
//                                                                                                  //
// ------------------------------------------------------------------------------------------------ //
//////////////////////////////////////////////////////////////////////////////////////////////////////

public class InteractManager : MonoBehaviour
{
    public Flowchart fc;
    
    private CameraManager mainCam;
    public AudioClip selectSFX;
    public AudioClip deselectSFX;

    //public static bool selected;
    public string selected;
    public Interactable[] interactableObjects;

    public bool hovering;  // true if player is hovering over an interactable object
    public bool shifting;

    public Vector3 prevMousePos;

    // Start is called before the first frame update
    void Start()
    {
        selected = "";

        fc = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        
        // getting SFX from resources
        selectSFX = Resources.Load("select") as AudioClip;
        deselectSFX = Resources.Load("deselect") as AudioClip;

        hovering = false;
        shifting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void Deselect(GameObject obj)
    {
        // only deselect if clicked on the selected object
        if (selected == obj.name)
        {
            //selected = false;
            selected = "";
            
            mainCam.cameraReset();
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
        interactableObjects = Object.FindObjectsOfType<Interactable>();
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
        shifting = false;
        if (!hovering && !fc.GetBooleanVariable("locked") && selected == "")
        {
            mainCam.init_camPos = new Vector3(mainCam.transform.position.x,
                                              mainCam.transform.position.y,
                                              -10 );
            //mainCam.cameraLocked = false;
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
