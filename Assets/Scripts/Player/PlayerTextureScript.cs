using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerTextureScript : MonoBehaviour
{
    
    [SerializeField] Texture2D _texture;
    [SerializeField] MeshRenderer _meshRenderer;

    Material _material = null;

    private void Start()
    {
        _material = new Material(_meshRenderer.sharedMaterial);
        _meshRenderer.sharedMaterial = _material;
    }

    void Update()
    {
        if (_material == null)
        {
            _meshRenderer.sharedMaterial.mainTexture = _texture;
        }
        else {
            _material.mainTexture = _texture;
        }
    }
}
