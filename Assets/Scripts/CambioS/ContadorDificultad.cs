using System;
using UnityEngine;

public class NivelManager : MonoBehaviour
{
    public static NivelManager Instancia;

    [Header("Nivel inicial (editable en inspector)")]
    [SerializeField]
    private float nivelInicial = 0;

    public float NivelActual { get; private set; }

    public event Action<float> OnNivelCambiado;

    private void Awake()
    {
        Instancia = this;

        // El nivel inicia con el valor del inspector
        NivelActual = nivelInicial;
    }

    public void SumarNivel()
    {
        NivelActual = NivelActual+0.1f;
        OnNivelCambiado?.Invoke(NivelActual);
    }
}
