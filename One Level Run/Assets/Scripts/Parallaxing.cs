using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{

    public Transform[] backgrounds;   // Array of all the back and forgrounds to be parallaxed
    private float[] parallaxScales;   // The proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;      // How smooth parallax is going to be. Set above 0.

    private Transform cam;
    private Vector3 previousCamPos;   // the position of the camera in the previous frame

    //Called before Start()
    void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCamPos = cam.position;

        parallaxScales = new float[backgrounds.Length];
        for(int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set target x position which is current position + the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // create target position which is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // fade between current position and target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        // set previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
