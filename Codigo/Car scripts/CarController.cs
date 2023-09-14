using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public WheelScript[] wheels;
    public float wheelBase;
    public float rearTrack;
    public float turnRadius;
    public float steerInput;
    [SerializeField] private  float ackermanAngleLeft;
   [SerializeField] private  float ackermanAngleRight;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        steerInput=Input.GetAxis("Horizontal");
        if (steerInput>0) //girando derecha
        {
            ackermanAngleLeft=Mathf.Rad2Deg * Mathf.Atan(wheelBase/(turnRadius+(rearTrack/2)))*steerInput;
            
            ackermanAngleRight=Mathf.Rad2Deg * Mathf.Atan(wheelBase/(turnRadius-(rearTrack/2)))*steerInput;
        }else if (steerInput<0) //girando izquierda 
        {
            ackermanAngleLeft=Mathf.Rad2Deg * Mathf.Atan(wheelBase/(turnRadius-(rearTrack/2)))*steerInput;
            
            ackermanAngleRight=Mathf.Rad2Deg * Mathf.Atan(wheelBase/(turnRadius+(rearTrack/2)))*steerInput;
            
        }else   //no esta girando
        {
            ackermanAngleLeft=0;
            ackermanAngleRight=0;
        }

        foreach (WheelScript w in wheels)
        {
            if (w.frontLeft)
            {
                w.wheelAngle=ackermanAngleLeft;
            }
            
            if (w.frontRight)
            {
                w.wheelAngle=ackermanAngleRight;
            }
        }
    }
}
