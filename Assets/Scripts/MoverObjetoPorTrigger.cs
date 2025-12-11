using UnityEngine;

public class MoverObjetoPorDistancia : MonoBehaviour
{
    [Header("Objeto que se va a mover")]
    public Transform objetoAMover;

    [Header("Tres posiciones posibles para el teletransporte")]
    public Transform[] posicionesDestino = new Transform[3];
    public Transform[] posicionesDestino2 = new Transform[3];

    [Header("Distancia base")]
    public float distanciaBase = 5f;
    public float distanciaMinima = 1f;

    [Header("Opciones adicionales")]
    public bool moverSoloUnaVez = true;

    private Transform objetoDetector;
    private bool yaMovio = false;

    private float nivelActual = 0;

    public EntidadData entidadData;

    public Animator animator;
    public AnimationClip vibra;
    public AnimationClip idle;

    public Animator portal;
    public AnimationClip clipPortal;

    public ParticleSystem estrellas;
    private bool yaEvaluoProbabilidad = false;

    private void OnEnable()
    {
        // Registrar eventos del manager si existe
        if (NivelManager1.Instancia != null)
        {
            nivelActual = NivelManager1.Instancia.NivelActual;
            NivelManager1.Instancia.OnNivelCambiado += ActualizarNivel;
        }
    }

    private void OnDisable()
    {
        if (NivelManager1.Instancia != null)
            NivelManager1.Instancia.OnNivelCambiado -= ActualizarNivel;
    }

    // Se ejecuta cada vez que cualquier script cambia el nivel
    private void ActualizarNivel(float nuevoNivel)
    {
        nivelActual = nuevoNivel;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            objetoDetector = playerObj.transform;
    }

    private void Update()
    {
        if (objetoAMover == null || objetoDetector == null)
            return;

        if (moverSoloUnaVez && yaMovio)
            return;

        float distanciaRequerida = distanciaBase / (1f + nivelActual);
        float distanciaActual = Vector3.Distance(objetoDetector.position, transform.position);

        // Asegura que nunca sea menor a la mínima
        distanciaActual = Mathf.Max(distanciaActual, distanciaMinima);

        if (distanciaActual <= distanciaRequerida)
        {
            if (!yaEvaluoProbabilidad)
            {
                yaEvaluoProbabilidad = true;

                // 75% de probabilidad
                if (Random.Range(0, 4) != 3)
                    SetVibra();
            }
        }
        else
        {
            // Se resetea al salir del rango
            yaEvaluoProbabilidad = false;
        }
    }

    private void MoverObjeto()
    {
        estrellas.Play();
        portal.Play(clipPortal.name);

        entidadData.Morir();

        if (posicionesDestino.Length != 3)
        {
            Debug.LogError("Debes asignar exactamente 3 posiciones destino.");
            return;
        }

        // Calcular posición más cercana
        float[] distancias = new float[3];
        for (int i = 0; i < 3; i++)
            distancias[i] = Vector3.Distance(objetoAMover.position, posicionesDestino[i].position);

        int indiceMasCercano = 0;
        for (int i = 1; i < 3; i++)
            if (distancias[i] < distancias[indiceMasCercano])
                indiceMasCercano = i;

        // Elegir uno de los otros dos destinos
        var indicesValidos = new System.Collections.Generic.List<int> { 0, 1, 2 };
        indicesValidos.Remove(indiceMasCercano);

        int indiceElegido = indicesValidos[Random.Range(0, indicesValidos.Count)];

        objetoAMover.position = posicionesDestino[indiceElegido].position;

        yaMovio = true;
    }

    public void SetIdle()
    {
        animator.Play(idle.name);
    }

    public void SetVibra()
    {
        animator.Play(vibra.name);
    }

    public void Mover2()
    {
        estrellas.Play();
        portal.Play(clipPortal.name);


        if (posicionesDestino2.Length != 3)
        {
            Debug.LogError("Debes asignar exactamente 3 posiciones destino.");
            return;
        }

        // Calcular posición más cercana
        float[] distancias = new float[3];
        for (int i = 0; i < 3; i++)
            distancias[i] = Vector3.Distance(objetoAMover.position, posicionesDestino2[i].position);

        int indiceMasCercano = 0;
        for (int i = 1; i < 3; i++)
            if (distancias[i] < distancias[indiceMasCercano])
                indiceMasCercano = i;

        // Elegir uno de los otros dos destinos
        var indicesValidos = new System.Collections.Generic.List<int> { 0, 1, 2 };
        indicesValidos.Remove(indiceMasCercano);

        int indiceElegido = indicesValidos[Random.Range(0, indicesValidos.Count)];

        objetoAMover.position = posicionesDestino2[indiceElegido].position;

        yaMovio = true;
    }
#if UNITY_EDITOR
   /* private void OnDrawGizmosSelected()
    {
        if (objetoDetector == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                objetoDetector = playerObj.transform;
        }

        // Calcula la distancia requerida según el nivel
        float distanciaRequerida = distanciaBase / (1f + nivelActual);

        // Dibuja una esfera en la posición del objeto que indica el rango
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f); // verde translúcido
        Gizmos.DrawSphere(transform.position, distanciaRequerida);

        // También dibuja la distancia mínima para referencia
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f); // rojo translúcido
        Gizmos.DrawSphere(transform.position, distanciaMinima);
    }*/
#endif
}
