using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;

public class FollowSpline2D : MonoBehaviour
{
    [SerializeField] private SplineContainer mainSplineContainer;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float offsetZ = 0f;
    [SerializeField, Range(0.05f, 0.3f)] private float switchSmoothTime = 0.12f;

    private float distanceTraveled = 0f;
    private float mainDistanceAtSwitch = 0f;

    // Transición
    private bool isSwitching = false;
    private float switchTimer = 0f;
    private float switchStartDistance = 0f;

    private SplineContainer fromSpline;
    private SplineContainer toSpline;
    private float fromSplineLength;  // Longitud del spline de origen en el momento del switch
    private float toSplineLength;    // Longitud del spline destino

    // Cache para modo normal (evitamos recalcular longitudes cada frame)
    private SplineContainer activeSpline;
    private float activeSplineLength;

    private void Start()
    {
        activeSpline = mainSplineContainer;
        activeSplineLength = mainSplineContainer.CalculateLength();
        fromSpline = toSpline = mainSplineContainer;
    }

    private void Update()
    {
        distanceTraveled += speed * Time.deltaTime;

        if (isSwitching)
        {
            switchTimer += Time.deltaTime;
            float t = Mathf.Clamp01(switchTimer / switchSmoothTime);

            // t local en cada spline
            float tFrom = (switchStartDistance - GetSplineStartDistance(fromSpline)) / fromSplineLength;
            float tTo = (distanceTraveled - GetSplineStartDistance(toSpline)) / toSplineLength;

            // Clamp por seguridad
            tFrom = Mathf.Clamp01(tFrom);
            tTo = Mathf.Clamp01(tTo);

            Vector3 posFrom = fromSpline.EvaluatePosition(tFrom);
            Vector3 posTo = toSpline.EvaluatePosition(tTo);
            Vector3 tanFrom = math.normalize(fromSpline.EvaluateTangent(tFrom));
            Vector3 tanTo = math.normalize(toSpline.EvaluateTangent(tTo));

            // Interpolación súper suave (cúbica con tangentes)
            Vector3 position = HermiteBlend(posFrom, tanFrom * fromSplineLength, posTo, tanTo * toSplineLength, t);
            Vector3 forward = Vector3.Slerp(tanFrom, tanTo, t).normalized;

            transform.position = new Vector3(position.x, position.y, offsetZ);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);

            if (t >= 1f)
            {
                isSwitching = false;
                activeSpline = toSpline;
                activeSplineLength = toSplineLength;
            }
        }
        else
        {
            // Modo normal – súper limpio
            float t = (distanceTraveled - GetSplineStartDistance(activeSpline)) / activeSplineLength;
            t = Mathf.Clamp01(t);

            Vector3 pos = activeSpline.EvaluatePosition(t);
            Vector3 tan = math.normalize(activeSpline.EvaluateTangent(t));

            transform.position = new Vector3(pos.x, pos.y, offsetZ);
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(tan.y, tan.x) * Mathf.Rad2Deg);
        }
    }

    // Devuelve la distancia global donde empieza este spline
    private float GetSplineStartDistance(SplineContainer spline)
    {
        return spline == mainSplineContainer ? 0f : mainDistanceAtSwitch;
    }

    // Interpolación Hermite real (la que usan los trenes de alta velocidad y rollercoasters)
    private Vector3 HermiteBlend(Vector3 p0, Vector3 m0, Vector3 p1, Vector3 m1, float t)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float h00 = 2 * t3 - 3 * t2 + 1;  // 1 at 0, 0 at 1
        float h10 = t3 - 2 * t2 + t;  // 0 at 0, 0 at 1, derivada 1 at 0
        float h01 = -2 * t3 + 3 * t2;      // 0 at 0, 1 at 1
        float h11 = t3 - t2;    // 0 at 0, 0 at 1, derivada 1 at 1

        return h00 * p0 + h10 * m0 + h01 * p1 + h11 * m1;
    }

    public void OnBranchTriggerEntered(BranchTrigger trigger)
    {
        NivelManager1.Instancia.SubirNivel();
        speed = speed+0.05f;    

        if (isSwitching || activeSpline != mainSplineContainer) return;

        int lever = Manager.Instance.leverState;
        SplineContainer next = mainSplineContainer;

        if (lever == 1 && trigger.downBranch) next = trigger.downBranch;
        else if (lever == 2 && trigger.upBranch) next = trigger.upBranch;

        if (next == mainSplineContainer) return;

        // ¡¡TRANSICIÓN PERFECTA!!
        mainDistanceAtSwitch = distanceTraveled;

        fromSpline = activeSpline;
        toSpline = next;

        fromSplineLength = activeSplineLength;
        toSplineLength = next.CalculateLength();

        switchStartDistance = distanceTraveled;
        switchTimer = 0f;
        isSwitching = true;
    }

    public void OnMergeTriggerEntered(MergeTrigger trigger)
    {
       
        if (isSwitching || activeSpline == mainSplineContainer) return;

        fromSpline = activeSpline;
        toSpline = mainSplineContainer;

        fromSplineLength = activeSplineLength;
        toSplineLength = mainSplineContainer.CalculateLength();

        switchStartDistance = distanceTraveled;
        switchTimer = 0f;
        isSwitching = true;

        Manager.Instance.ResetLever();
    }

    // Propiedades públicas que ya tenías
    public float DistanceTraveled => distanceTraveled;
    public SplineContainer CurrentContainer => isSwitching ? toSpline : activeSpline;
    public float Speed => speed;
}