using UnityEngine;

public class Trenmovimiento : MonoBehaviour
{
    [SerializeField] public float velocidad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = transform.position + new Vector3(velocidad,0,0) * Time.deltaTime;
    }
}
