using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class ForceTimeScale : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 1f;
    }
}
