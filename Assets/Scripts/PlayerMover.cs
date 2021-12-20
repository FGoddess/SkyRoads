using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    [SerializeField] private float _rotationSmooth = 10f;
    private float _rotationAngle = 45f;

    private float _xBoundsPoition = 3.5f;

    private bool _isDead;

    [SerializeField] private Score _score; // REMOVE THIS

    private void Update()
    {
        if (_isDead) return;

        var horizontalInput = Input.GetAxisRaw("Horizontal");

        transform.Translate(new Vector3(horizontalInput, 0, 0) * _speed * Time.deltaTime, Space.World);

        var angle = horizontalInput == 0 ? 0 : horizontalInput < 0 ? _rotationAngle : -_rotationAngle;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), _rotationSmooth * Time.deltaTime);

        if (transform.position.x > _xBoundsPoition || transform.position.x < -_xBoundsPoition)
        {
            var temp = transform.position.x > _xBoundsPoition ? _xBoundsPoition : -_xBoundsPoition;
            transform.position = new Vector3(temp, transform.position.y, transform.position.z);
        }
    }

    public void Die()
    {
        _isDead = true;
        _score.SaveHighScore();
    }

}
