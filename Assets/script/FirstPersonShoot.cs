using UnityEngine;

public class FirstPersonShoot : MonoBehaviour
{   
    public Transform particulaTiro;
    public Transform ancoratiro;
    float distanciatiro = Mathf.Infinity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
          Atira();
        }
     
    }

    void Atira()
    {
        Transform instanciado = Instantiate(particulaTiro);
        instanciado.position = ancoratiro.position;
        instanciado.rotation = Quaternion.Euler(ancoratiro.rotation.eulerAngles + new Vector3(0,90,0));
        instanciado.parent = ancoratiro;
        Ray raio = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(raio, out hit, distanciatiro))
        {
            if (hit.transform.TryGetComponent<V>(out V inimigo)) 
            {
                inimigo.ReceberDano(1);

                Vector3 direcaoTiro = hit.transform.position - Camera.main.transform.position;
                inimigo.transform.GetComponent<Rigidbody>().AddForce(direcaoTiro * 5, ForceMode.Impulse);   
            }
        }
    }
}
