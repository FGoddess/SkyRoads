using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Renderer _renderer;

    private float _speed = 2f;
    private float _maxSpeed = 4.5f;

    private float _maxYOffset = 10f;
    private float _yOffset = 0;

    [SerializeField] private DifficultyManager _difficultyManager;

    private void OnEnable()
    {
        _difficultyManager.MultiplierChanged += OnDifficultyMultiplierChanged;
    }

    private void OnDisable()
    {
        _difficultyManager.MultiplierChanged -= OnDifficultyMultiplierChanged;
    }

    private void OnDifficultyMultiplierChanged(float value)
    {
        _speed += value;

        if(_speed > _maxSpeed)
        {
            OnDisable();
        }
    }

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _yOffset += Time.deltaTime * _speed;

        if(_yOffset > _maxYOffset)
        {
            _yOffset = 0;
        }

        _renderer.material.SetTextureOffset("_MainTex", new Vector2(1, -_yOffset));
        _renderer.material.SetTextureOffset("_SpecTex", new Vector2(1, -_yOffset));
        _renderer.material.SetTextureOffset("_NormalTex", new Vector2(1, -_yOffset));
        _renderer.material.SetTextureOffset("_EmissionTex", new Vector2(1, -_yOffset));
    }
}
