using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{


    public bool frontLeft, rearLeft, frontRight,rearRight;
    private Rigidbody rb;
    private const float restLenght=0.5f;
    private const float springTravel=0.2f;
    private const float wheelRadius=0.6f;
  
    private float minLeght;
    private float maxLenght;
    private float springLenght;
  
    private float springForce;
    private Vector3 suspensionForce;
    private float springVelocity;
    private float damperForce;
    private float lastLenght; 
    private float wheelAngle;
    private float fixedAngle;

    private float fuerzaEnX;
    private float fuerzaEnY;
    private Vector3 localWheelVelocity;
    private BoxCollider bc;

    [Header("Valores modificables")]
        public float  forceApplied; //en porcentaje
        public float springStiffnes;
        public float TimeToSteer;
         public float damperStiffnes;
        
    void Start()
    {   rb= this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
         bc= this.transform.parent.gameObject.transform.parent.gameObject.transform.GetChild(2).GetComponent<BoxCollider>();
        minLeght= restLenght-springTravel;
        maxLenght= restLenght+springTravel;
    }

      void Update() {
     
        fixedAngle= Mathf.Lerp(fixedAngle, wheelAngle, TimeToSteer*Time.deltaTime);
        transform.localRotation= Quaternion.Euler(transform.localRotation.x, transform.localRotation.y +fixedAngle,transform.localRotation.z);
        Debug.DrawRay(transform.GetChild(0).transform.position, -transform.up *(maxLenght+wheelRadius),Color.white);

    }
    void FixedUpdate()
    {
  
        
        if (Physics.Raycast(transform.GetChild(0).transform.position, -transform.up, out RaycastHit hit, maxLenght+wheelRadius))
    {   lastLenght=springLenght;
        springLenght=hit.distance-wheelRadius;
        springLenght=Mathf.Clamp(springLenght, minLeght, maxLenght);


        springVelocity=(lastLenght- springLenght)/Time.fixedDeltaTime;
        
        springForce= springStiffnes*(restLenght-springLenght);
        damperForce= damperStiffnes*springVelocity;
        suspensionForce= (springForce+damperForce)*transform.up;


        localWheelVelocity=transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));

        fuerzaEnX= -Input.GetAxis("Vertical")*springForce*(forceApplied/100);
        fuerzaEnY= localWheelVelocity.x *springForce;
        // fuerzaEnY=0;




           rb.AddForceAtPosition(suspensionForce+ (fuerzaEnX*transform.forward)+ (fuerzaEnY*-transform.right), hit.point);
     }
     Debug.Log("current velocity "+rb.velocity.magnitude*3.6f);

    }

    public void setWheelAngle(float aux){
        this.wheelAngle=aux;
    }
    public float getSpeed(){
        return rb.velocity.magnitude*3.6f;
    }
}
