using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private Renderer _renderer;
    private float _maxYOffset = 10f;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    float offsetY = 0;

    private void Update()
    {
        offsetY += Time.deltaTime;

        if(offsetY > _maxYOffset)
        {
            offsetY = 0;
        }

        _renderer.material.SetTextureOffset("_MainTex", new Vector2(1, -offsetY));
        _renderer.material.SetTextureOffset("_SpecTex", new Vector2(1, -offsetY));
        _renderer.material.SetTextureOffset("_NormalTex", new Vector2(1, -offsetY));
        _renderer.material.SetTextureOffset("_EmissionTex", new Vector2(1, -offsetY));
    }
}
