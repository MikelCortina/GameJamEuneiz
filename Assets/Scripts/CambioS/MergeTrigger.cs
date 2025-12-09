using UnityEngine;

public class MergeTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<FollowSpline2D>(out var f))
            f.OnMergeTriggerEntered(this);
    }
}