using System;
using UnityEngine;

public class NivelManager1 : MonoBehaviour
{
    public static NivelManager1 Instancia { get; private set; }

    // Evento que envía el nuevo nivel cuando cambia
    public event Action<float> OnNivelCambiado;

    [SerializeField]
    private float nivelActual = 0f;

    public float NivelActual
    {
        get => nivelActual;
        set
        {
            if (Mathf.Approximately(nivelActual, value))
                return;

            nivelActual = value;
            OnNivelCambiado?.Invoke(nivelActual);
        }
    }

    private void Awake()
    {
        // Singleton seguro
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        Instancia = this;
        DontDestroyOnLoad(gameObject);
    }

    // Métodos útiles
    public void SubirNivel(float cantidad = 0.05f)
    {
        NivelActual += cantidad;
    }

    public void BajarNivel(float cantidad = 1f)
    {
        NivelActual = Mathf.Max(0f, NivelActual - cantidad);
    }

    public void EstablecerNivel(float nuevoNivel)
    {
        NivelActual = nuevoNivel;
    }
}

