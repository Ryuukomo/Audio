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
    public Animator animator;
    CharacterController characterController;
    public AudioSource passos;
    
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
        AnimaAnda();
        abaixar();
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
          if (movimento.magnitude > 0.1f)
        {
            if (passos.isPlaying == false)
            {
                passos.Play();
            }


        }
        else
            {
                passos.Stop();
            }

        //characterController.Move(movimento * Time.deltaTime * velocidadeMovimento);
      
        if (velocidade.y < 0 && estachao == true) 
        {
            velocidade.y = -1f;
        }

        if(Input.GetButtonDown("Jump") && estachao == true)
        {
            velocidade.y = Mathf.Sqrt(1.5f * -2f * gravidade);
        }
        velocidade.y += gravidade * Time.deltaTime;
        
        Vector3 movimentofinal = (movimento * velocidadeMovimento + velocidade) * Time.deltaTime;

        characterController.Move(movimentofinal);
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

    public void tiro()
    {
        animator.SetTrigger("tiro");
    }

    void abaixar ()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            characterController.height = 1.25f;
        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            characterController.height = 2f;
        }
    }
    void AnimaAnda()
    {
        Vector3 velocidadeY = characterController.velocity;
        velocidadeY.y = 0;

        animator.SetFloat("andando", velocidadeY.magnitude);
        //if(characterController.velocity.magnitude > 0.1f)
        //{
        //    animator.SetBool("andando", true);
        //}
        //else
        //{
        //    animator.SetBool("andando", false);
        //}
    }
}
