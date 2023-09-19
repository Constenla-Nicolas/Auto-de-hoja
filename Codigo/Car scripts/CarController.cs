using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public WheelScript[] wheels;
    private const float  wheelBase=2.8f;
    private const float rearTrack= 1.5748f;
    private const float turnRadius=5.85216f;
    public float minSpeedForMaxSteering;
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

 
        // Calculate individual wheel angles using the Ackermann steering formula
        float frontLeftWheelAngle =  Mathf.Lerp(ackermanAngleLeft, 8f*steerInput, wheels[1].getSpeed() / minSpeedForMaxSteering);
        float frontRightWheelAngle =  Mathf.Lerp(ackermanAngleRight, 8f*steerInput, wheels[0].getSpeed() / minSpeedForMaxSteering);

        // Debug.Log(frontLeftWheelAngle +" , "+ frontRightWheelAngle);

        foreach (WheelScript w in wheels)
        {
            if (w.frontLeft)
            {
                w.setWheelAngle(frontLeftWheelAngle);
            }

            if (w.frontRight)
            {
                w.setWheelAngle(frontRightWheelAngle);
            }
        }
    }
}
