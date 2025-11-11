using Unity.VisualScripting;
using UnityEngine;

public class FirstPersonPlayer : MonoBehaviour
{
    public float velocidadeMovimento = 10f;
    float gravidade = -10;
    float sensibilidade = 2;
    float rotacao = 500;
    float delayBraco = 2;
    bool estachao = false;
 
    Transform pistol;
    Transform ancorapistola;
    CharacterController characterController;
    
    Vector3 velocidade; 
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        pistol = transform.Find("Pistol");
        ancorapistola = transform.Find("Main Camera/AncoraPistola");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        OlharDigital();
        MoveBraço();
    }

    void Movimento()
    {
        estachao = characterController.isGrounded;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // mudanças de Vector3 movimento = new Vector3(horizontal, 0, vertical);

        Vector3 movimento = transform.right * horizontal + transform.forward * vertical;

        if (movimento.magnitude > 1)
        {
            movimento = movimento.normalized;
        }
        
        characterController.Move(movimento * Time.deltaTime * velocidadeMovimento);

        if (velocidade.y < 0 && estachao == true) 
        {
            velocidade.y = -1f;
        }

        if(Input.GetButtonDown("Jump") && estachao == true)
        {
            velocidade.y = Mathf.Sqrt(1.5f * -2f * gravidade);
        }
        velocidade.y += gravidade * Time.deltaTime;
        characterController.Move(velocidade * Time.deltaTime);
    }

    void OlharDigital()
    {
        float mousex = Input.GetAxis("Mouse X") * sensibilidade;
        float mousey = Input.GetAxis("Mouse Y") * sensibilidade;

        rotacao -= mousey;
        rotacao = Mathf.Clamp(rotacao, -90f, 90); 
        Camera.main.transform.localRotation = Quaternion.Euler(rotacao,0,0);

        transform.Rotate(0, mousex, 0);
    }

    private void MoveBraço()
    {
        //pistol.rotation = ancorapistola.rotation;
        //pistol.position = ancorapistola.position;

        pistol.position = Vector3.Lerp(pistol.position, ancorapistola.position, delayBraco * Time.deltaTime);
        pistol.rotation = Quaternion.Lerp(pistol.rotation, ancorapistola.rotation, delayBraco * Time.deltaTime);
    }
}
