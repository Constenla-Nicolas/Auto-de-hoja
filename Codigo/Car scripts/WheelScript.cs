using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelScript : MonoBehaviour
{   public bool frontLeft, rearLeft, frontRight,rearRight;
    private Rigidbody rb;
    public float restLenght;
    public float springTravel;
    public float springStiffnes;
    private float minLeght;
    private float maxLenght;
    private float springLenght;
    public float wheelRadius;
    private float springForce;
    private Vector3 suspensionForce;
    private float springVelocity;
    private float damperForce;
    private float lastLenght;
    public float damperStiffnes;
    public float wheelAngle;
    public float fixedAngle;
    public float TimeToSteer; 
    private float fuerzaEnX;
    private float fuerzaEnY;
    private Vector3 localWheelVelocity;
    void Start()
    {   rb= this.transform.parent.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>();
        // Debug.DrawRay(transform.position, -transform.up, Color color = Color.white);
        minLeght= restLenght-springTravel;
        maxLenght= restLenght+springTravel; 
    }

      void Update() {
        fixedAngle= Mathf.Lerp(fixedAngle, wheelAngle, TimeToSteer*Time.deltaTime);
        transform.localRotation= Quaternion.Euler(transform.localRotation.x, transform.localRotation.y +fixedAngle,transform.localRotation.z);
 
    }
    void FixedUpdate()
    {   
        if (Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, maxLenght+wheelRadius))
    {   lastLenght=springLenght;
        springLenght=hit.distance-wheelRadius;
        springLenght=Mathf.Clamp(springLenght, minLeght, maxLenght);
        springVelocity=(lastLenght- springLenght)/Time.fixedDeltaTime;
        springForce= springStiffnes*(restLenght-springLenght);
        damperForce= damperStiffnes*springVelocity;
        suspensionForce= (springForce+damperForce)*transform.up;
        
        
        localWheelVelocity=transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
     



        fuerzaEnX= -Input.GetAxis("Vertical")*springForce;
        fuerzaEnY= localWheelVelocity.x *springForce;

       


           rb.AddForceAtPosition(suspensionForce+ (fuerzaEnX*transform.forward)+ (fuerzaEnY*-transform.right), hit.point);
            Debug.Log("velocidad= "+suspensionForce+ (fuerzaEnX*transform.forward)+ (fuerzaEnY*-transform.right));
    }

    }
}
