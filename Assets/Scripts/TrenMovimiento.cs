using Unity.VisualScripting;
using UnityEngine;

public class TrenMovimiento : MonoBehaviour
{
    [SerializeField] public float velocidad;
    public GameManager gameManager;
    public GameObject resetPoint;
    public GameObject actionPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = transform.position + new Vector3(velocidad,0,0) * Time.deltaTime;//Movimiento eje x
    }
    void IrPorArriba()
    {
        this.transform.position = this.transform.position + new Vector3(0, 3, 0);
    }
    void IrPorAbajo()
    {
        this.transform.position = this.transform.position + new Vector3(0, -3, 0);
    }
    void IrPorElMedio()
    {
        transform.position = new Vector3(
        transform.position.x,
        0,
        transform.position.z
    );
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ResetPoint"))
        {
            Debug.Log("RESET");
            gameManager.decision = 0; //RESETEO DE LA DECISION
            resetPoint.transform.position += new Vector3(40, 0, 0);//MOVER EL RESETPOINT
            IrPorElMedio();
        }
        if (collision.CompareTag("ActionPoint"))
        {
            Debug.Log("ACCION");
            //ACCION
            actionPoint.transform.position += new Vector3(40, 0, 0);//MOVER EL ACTIONPOINT
            if (gameManager.decision == 1)
            {
                IrPorArriba();
            }
            if (gameManager.decision == 2)
            {
                IrPorAbajo();
            }
        }
    }
}
