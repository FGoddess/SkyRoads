using System.Collections;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public float distance = 10.0f;
    public float height = 5.0f;
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;
    public Transform target;

    [SerializeField] private float _zoomedDistance = 5f;
    [SerializeField] private float _zoomedHeight = 2.5f;

    [SerializeField] private float _timeToZoom = 1f;

    private Coroutine _coroutine;
    private float _initialHeight;
    private float _initialDistance;

    private void Start()
    {
        _initialDistance = distance;
        _initialHeight = height;
    }

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = target.position - currentRotation * Vector3.forward * distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(target);
    }

    public void ChangeCameraView(bool zoomIn)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(LerpCamera(zoomIn));
    }

    private IEnumerator LerpCamera(bool zoomIn)
    {
        float timeElapsed = 0;

        var targetHeight = zoomIn ? _zoomedHeight : _initialHeight;
        var targetDistance = zoomIn ? _zoomedDistance : _initialDistance;

        while (timeElapsed < _timeToZoom)
        {
            height = Mathf.Lerp(height, targetHeight, timeElapsed / _timeToZoom);
            distance = Mathf.Lerp(distance, targetDistance, timeElapsed / _timeToZoom);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        height = targetHeight;
        distance = targetDistance;

        _coroutine = null;

    }
}