using UnityEngine;

public class SweepCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<ISweepable>()?.OnSweeped();
    }
}