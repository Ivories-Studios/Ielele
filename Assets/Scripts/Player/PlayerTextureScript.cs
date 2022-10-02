using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTextureScript : MonoBehaviour
{
    
    [SerializeField] Texture2D _texture;
    [SerializeField] MeshRenderer _meshRenderer;

    Material _material = null;

    void Update()
    {
        if (_material == null)
        {
            _material = new Material(_meshRenderer.sharedMaterial);
            _meshRenderer.sharedMaterial = _material;
        }

        _material.mainTexture = _texture;
    }
}
