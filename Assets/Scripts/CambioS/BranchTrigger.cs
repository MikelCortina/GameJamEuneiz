using UnityEngine;
using UnityEngine.Splines;

public class BranchTrigger : MonoBehaviour
{
    public SplineContainer upBranch;
    public SplineContainer downBranch;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<FollowSpline2D>(out var f))
            f.OnBranchTriggerEntered(this);
    }
}