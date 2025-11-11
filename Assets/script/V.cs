using UnityEngine;

public class V : MonoBehaviour
{
    int vida = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceberDano(int valor)
    {
        vida -= valor;

        if (vida < 0) 
        { 
        Destroy(gameObject);        
        }
    }
}
