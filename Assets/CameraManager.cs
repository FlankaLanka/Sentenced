using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to control Camera Movement
// currently only used to control camera zoom on selected objects

public class CameraManager : MonoBehaviour
{

    private Camera cam;
    private Vector3 origin_camPos;
    public Vector3 init_camPos;
    public Vector3 target;
    public bool cameraLocked;

    // make sure player cannot drag camera out of scene
    public float minx; 
    public float maxx; 

    public float miny; 
    public float maxy;


    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        origin_camPos = this.transform.position;
        init_camPos = this.transform.position;
        target = this.transform.position;
        cameraLocked = false;

        // temporary hard-coded values 
        //-- IDEALLY INSTANTIATE THESE IN THE EDITOR AND COMMENT THIS CODE OUT --//
        minx = -15;
        maxx = 15;
        miny = 0f;
        maxy = 0f;
    }

    // Update is called once per frame
    void Update()
    {   
        if (!cameraLocked) {
            this.transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 4);
        }
        
        // dealing with scene scrolling
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 temp = this.transform.position;
            temp = temp + new Vector3(-.1f, 0, 0);
            this.transform.position = new Vector3(
                    Mathf.Min(Mathf.Max(temp.x, minx), maxx),  
                    Mathf.Min(Mathf.Max(temp.y, miny), maxy), 
                    temp.z
                );
            init_camPos = temp;
            cameraLocked = true;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 temp = this.transform.position;
            temp = temp + new Vector3(.1f, 0, 0);
            this.transform.position = new Vector3(
                    Mathf.Min(Mathf.Max(temp.x, minx), maxx),  
                    Mathf.Min(Mathf.Max(temp.y, miny), maxy), 
                    temp.z
                );
            init_camPos = temp;
            cameraLocked = true;
        }
        
    }

    // zooms in the camera on a specific gameObject position
    public void cameraZoom(Vector3 toZoomPosition)
    {
        cameraLocked = false;
        // find vector 3 for distance to travel
        //Vector3 dist = new Vector3(toZoomPosition.x, toZoomPosition.x, 0) ;

        if(target != init_camPos)
            Debug.Log("Changing Targets while target is already set");


        // only go half-way to z so that we can still see the object
        // Vector3 dist = new Vector3( (toZoomPosition.x - init_camPos.x) * .5f, 
        //                             (toZoomPosition.y - init_camPos.y) * .5f, 
        //                             (init_camPos.z - toZoomPosition.z) * .75f );
        Vector3 dist = new Vector3( 
            Mathf.Min(Mathf.Max(toZoomPosition.x, minx+(-10 - (init_camPos.z - toZoomPosition.z) * .75f)), maxx-(-10 - (init_camPos.z - toZoomPosition.z) * .75f)), 
            Mathf.Min(Mathf.Max(toZoomPosition.y, miny+(-10 - (init_camPos.z - toZoomPosition.z) * .75f)), maxy-(-10 - (init_camPos.z - toZoomPosition.z) * .75f)), 
            (init_camPos.z - toZoomPosition.z) * .75f );
        //target = new Vector3(toZoomPosition.x, toZoomPosition.y, init_camPos.z / 2f);
        target = dist;
    }

    // Sets target to initial camera position
    public void cameraReset()
    {
        cameraLocked = false;
        target = init_camPos;
    }
}
