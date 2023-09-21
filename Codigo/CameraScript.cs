using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{   public Camera camera;
    public Vector3 originalpos,backposition,backrotation;

    // Start is called before the first frame update
    void Start()
    {
         originalpos=new Vector3(0.012f,0.797f, 0.713f);
         backposition=new Vector3(0,0.772f,-1.161f);
        camera= GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
    {
  
        camera.transform.localPosition=backposition;
        transform.Rotate(new Vector3(0,180,0));
 
    }
    else if(Input.GetKeyUp(KeyCode.Space))
    {
             transform.Rotate(new Vector3(0,180,0));
        camera.transform.localPosition=originalpos;
    }


    }
}
