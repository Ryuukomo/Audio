using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

public class ThirdPersonCam : MonoBehaviour
{
    public Transform target;
    public float sense = 300;
    public float pitch;
    public float yaw;
    /*public float roll;*/
    Vector3 offset;
    void Start()
    {
        offset = transform.position - target.position;
        Cursor.lockState = CursorLockMode.Locked;
     
    }

    // Update is called once per frame
    void Update()
    {
     
        //transform.LookAt(target);
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;

        float mouseX = Input.GetAxis("Mouse X") * sense * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sense * Time.deltaTime;

        pitch -= mouseY;
        yaw += mouseX;
        //transform.rotation = Quaternion.Euler(pitch, 0, 0);

        //transform.rotation = Quaternion.Euler(0, yaw, 0);

       

        //transform.rotation = Quaternion.Euler(pitch, yaw, 0);
        Quaternion rotacao = Quaternion.Euler(pitch, yaw, 0);
        transform.position = target.position + rotacao * offset;
        transform.LookAt(target);

    }

}
