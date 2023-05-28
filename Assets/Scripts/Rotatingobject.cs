using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Rotatingobject : MonoBehaviour
{
    [SerializeField] float RotationConstantX = 100.0f;
    [SerializeField] float RotationConstantY = 100.0f;
    [SerializeField] float RotationConstantZ = 100.0f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(RotationConstantX * Time.deltaTime, RotationConstantY * Time.deltaTime, RotationConstantZ * Time.deltaTime));
    }
}
