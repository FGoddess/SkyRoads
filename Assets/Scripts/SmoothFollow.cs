using System.Collections;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] private float _distance = 10.0f;
    [SerializeField] private float _height = 5.0f;
    [SerializeField] private float _heightDamping = 2.0f;
    [SerializeField] private float _rotationDamping = 3.0f;
    [SerializeField] private Transform _target;

    [SerializeField] private float _zoomedDistance = 5f;
    [SerializeField] private float _zoomedHeight = 2.5f;

    [SerializeField] private float _timeToZoom = 1f;

    private Coroutine _coroutine;
    private float _initialHeight;
    private float _initialDistance;

    private void Start()
    {
        _initialDistance = _distance;
        _initialHeight = _height;
    }

    private void LateUpdate()
    {
        // Early out if we don't have a target
        if (!_target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = _target.eulerAngles.y;
        float wantedHeight = _target.position.y + _height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        var pos = transform.position;
        pos = _target.position - currentRotation * Vector3.forward * _distance;
        pos.y = currentHeight;
        transform.position = pos;

        // Always look at the target
        transform.LookAt(_target);
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
            _height = Mathf.Lerp(_height, targetHeight, timeElapsed / _timeToZoom);
            _distance = Mathf.Lerp(_distance, targetDistance, timeElapsed / _timeToZoom);
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        _height = targetHeight;
        _distance = targetDistance;

        _coroutine = null;

    }
}