using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class to control Camera Movement
// currently only used to control camera zoom on selected objects

public class CameraManager : MonoBehaviour
{

    private Camera cam;
    private Vector3 init_camPos;
    public Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        cam = this.GetComponent<Camera>();
        init_camPos = this.transform.position;
        target = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 2);
        
        //if (this.transform.position == target)
        //    target = null;
    }

    // zooms in the camera on a specific gameObject position
    public void cameraZoom(Vector3 toZoomPosition)
    {
        // find vector 3 for distance to travel
        //Vector3 dist = new Vector3(toZoomPosition.x, toZoomPosition.x, 0) ;

        if(target != init_camPos)
            Debug.Log("Changing Targets while target is already set");


        // only go half-way to z so that we can still see the object
        Vector3 dist = new Vector3( (toZoomPosition.x - init_camPos.x) * .5f, 
                                    (toZoomPosition.y - init_camPos.y) * .5f, 
                                    (init_camPos.z - toZoomPosition.z) * .75f );
        //target = new Vector3(toZoomPosition.x, toZoomPosition.y, init_camPos.z / 2f);
        target = dist;
    }

    // Sets target to initial camera position
    public void cameraReset()
    {
        target = init_camPos;
    }
}
