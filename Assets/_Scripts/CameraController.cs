using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform followTarget;
    [SerializeField] float distance = 8;

    private void Update()
    {

        transform.position = followTarget.position - new Vector3(0, 0, distance);
    }
    

}
