using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraFollowObject : MonoBehaviour
{
    [SerializeField] Transform _followTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(_followTransform.position.x, 0, 0);
    }
}
