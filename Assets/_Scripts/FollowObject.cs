using DG.Tweening;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform followObj;
    [SerializeField] private Vector3 offSet;
    [SerializeField] private float cameraFollowSpeed; 
    [SerializeField] private float transitionPos;
    [SerializeField] private float transitionRot;
    [SerializeField] private Vector3 finishlineCamPos;
    [SerializeField] private Vector3 finishlineCamRotEuler;

    private bool isFollowing;
    private Quaternion finishlineCamRot;
    private Vector3 target;

    private void Start()
    {
        ActionManager.OnFinishLine += ChangeCameraPosition;
    }

    private void Update()
    {

        target = new Vector3(0,0, followObj.transform.position.z) + offSet;
        transform.position = Vector3.Lerp(transform.position, target, 1);
        //transform.DOMove(new Vector3(offSet.x, offSet.y, target.z), cameraFollowSpeed * Time.deltaTime);
    }

    private void ChangeCameraPosition()
    {
        isFollowing = false;
        Debug.Log("CAMPOS ÇALIÞTI");
        finishlineCamRot = Quaternion.Euler(finishlineCamRotEuler);

        transform.DOMove(transform.position + finishlineCamPos, transitionPos)
            .SetEase(Ease.OutSine).OnComplete(()=> { offSet = finishlineCamPos; isFollowing = true; });

        transform.DORotateQuaternion(finishlineCamRot, transitionRot)
            .SetEase(Ease.OutSine);
    }

    private void OnDestroy()
    {
        ActionManager.OnFinishLine -= ChangeCameraPosition;
    }
}