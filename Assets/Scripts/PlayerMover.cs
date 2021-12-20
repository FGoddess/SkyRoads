using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _xBoundsPoition = 3.5f;

    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(horizontalInput, 0, 0) * _speed * Time.deltaTime);

        if (transform.position.x > _xBoundsPoition)
        {
            transform.position = new Vector3(_xBoundsPoition, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -_xBoundsPoition)
        {
            transform.position = new Vector3(-_xBoundsPoition, transform.position.y, transform.position.z);
        }

    }

}
