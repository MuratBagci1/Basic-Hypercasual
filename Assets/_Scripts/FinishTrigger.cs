using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private bool isComplete;

    private void OnTriggerEnter(Collider other)
    {
        if (isComplete)
                return;

        ActionManager.OnFinishLine?.Invoke();
        isComplete = true;
    }
}