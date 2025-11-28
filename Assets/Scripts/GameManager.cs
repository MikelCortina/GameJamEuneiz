using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int decision;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        { 
            decision = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            decision = 2;
        }
    }
}
