using System.Collections.Generic;
using UnityEngine;

public class FollowerWithDelay : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float delay = 0.5f;

    private struct Snapshot
    {
        public Vector3 pos;
        public Quaternion rot;
        public float time;

        public Snapshot(Vector3 p, Quaternion r, float t)
        {
            pos = p;
            rot = r;
            time = t;
        }
    }

    private readonly Queue<Snapshot> history = new Queue<Snapshot>();

    private void FixedUpdate()
    {
        // Agregar snapshot
        history.Enqueue(new Snapshot(target.position, target.rotation, Time.fixedTime));

        float targetTime = Time.fixedTime - delay;

        // Mantener solo snapshots recientes
        while (history.Count > 0 && history.Peek().time < targetTime - 1f) // margen grande para limpieza
        {
            history.Dequeue();
        }

        // Si no hay suficientes snapshots
        if (history.Count == 0) return;

        Snapshot A = default;
        Snapshot B = default;
        bool found = false;

        Snapshot prev = history.Peek();
        foreach (var snap in history)
        {
            if (snap.time >= targetTime)
            {
                A = prev;
                B = snap;
                found = true;
                break;
            }
            prev = snap;
        }

        if (!found)
        {
            // targetTime más reciente que todos los snapshots, usar el último
            var last = prev;
            transform.position = last.pos;
            transform.rotation = last.rot;
            return;
        }

        float t = Mathf.InverseLerp(A.time, B.time, targetTime);
        transform.position = Vector3.Lerp(A.pos, B.pos, t);
        transform.rotation = Quaternion.Slerp(A.rot, B.rot, t);
    }
}
