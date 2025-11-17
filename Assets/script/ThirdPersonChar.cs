using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class ThirdPersonChar : MonoBehaviour
{
   public float vel = 10;
    public float rotacao = 2;

   
    CharacterController controller;
    void Start()
    {
    controller = GetComponent<CharacterController>();
    
    }

    // Update is called once per frame
    void Update()
    {
        Movimente();
        
    }

    void Movimente()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

     
        Vector3 camright = Camera.main.transform.right;
        Vector3 camforward = Camera.main.transform.forward;

        camright.y = 0;
        camforward.y = 0;
       

        Vector3 movimento = camright * horizontal + camforward * vertical;
      
        
        //Vector3 movimento = Camera.main.transform.right * horizontal + Camera.main.transform.forward * vertical;
        //Vector3 movimento = new Vector3(horizontal, 0, vertical);
        if (movimento.magnitude > 0.1)
        {
            Quaternion destinoRot = Quaternion.LookRotation(movimento,Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, destinoRot, rotacao * Time.deltaTime);
        }
        
        movimento = movimento * vel * Time.deltaTime;
        controller.Move(movimento);

    }
}
