using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

// Class to manage Interactable Objects, Camera Zoom, and Fungus Text
public class InteractManager : MonoBehaviour
{
    public Flowchart fc;
    
    private CameraManager mainCam;
    public AudioClip selectSFX;
    public AudioClip deselectSFX;

    public static bool selected;
    public Interactable[] interactableObjects;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;

        fc = GameObject.Find("Flowchart").GetComponent<Flowchart>();

        mainCam = GameObject.FindWithTag("MainCamera").GetComponent<CameraManager>();
        selectSFX = Resources.Load("select") as AudioClip;
        deselectSFX = Resources.Load("deselect") as AudioClip;

        interactableObjects = Object.FindObjectsOfType<Interactable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select(GameObject obj)
    {
        if (!selected)
        {
            selected = true;
            mainCam.cameraZoom(obj.transform.position);


            // "lock" the scene upon getting clicked on
            fc.SetStringVariable("objName", obj.name);
            
            this.GetComponent<AudioSource>().PlayOneShot(selectSFX);
        }
        else
        {
            Debug.Log("Calling Select() with a selected object");
        }
    }

    public void Deselect()
    {
        if (selected)
        {
            selected = false;
            
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
        Debug.Log("Checking Interactables Status:");
        
        foreach (Interactable i in interactableObjects){
            Debug.Log(i.name + " interaction: " + i.interacted );
        }
    }
}
