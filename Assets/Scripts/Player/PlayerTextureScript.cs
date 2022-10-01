using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerTextureScript : MonoBehaviour
{
    
    [SerializeField] Texture2D _texture;
    [SerializeField] MeshRenderer _meshRenderer;

    void Update()
    {
        _meshRenderer.sharedMaterial.mainTexture = _texture;
    }
}
