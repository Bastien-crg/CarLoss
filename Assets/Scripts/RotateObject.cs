using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform objectPosition;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = objectPosition.rotation;
    }
}
