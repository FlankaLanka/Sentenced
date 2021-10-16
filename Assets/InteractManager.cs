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

    // Start is called before the first frame update
    void Start()
    {
        selected = "";

        fc = GameObject.Find("Flowchart").GetComponent<Flowchart>();
        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        
        // getting SFX from resources
        selectSFX = Resources.Load("select") as AudioClip;
        deselectSFX = Resources.Load("deselect") as AudioClip;
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
        
        foreach (Interactable i in interactableObjects){
            Debug.Log(i.name + " interaction: " + i.interacted );
        }
    }
}
