using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _moveSpeed = 10f;

    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed *= value; }

    private ParticleSystem _collisionParticle;

    private void Awake()
    {
        _collisionParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * _rotationSpeed * Time.deltaTime);
        transform.Translate(Vector3.back * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMover player))
        {
            StartCoroutine(PlayerCollision(player));
        }
    }

    private IEnumerator PlayerCollision(PlayerMover player)
    {
        _collisionParticle.Play();
        var mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;
        player.Die();
        yield return new WaitForSeconds(_collisionParticle.main.duration);
        gameObject.SetActive(false);
        mesh.enabled = true;
    }
}