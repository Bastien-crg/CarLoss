using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentB : MonoBehaviour
{
    void Awake()
    {
        MyTools.LOG(this, "Awake");
    }
    void OnEnable()
    {
        MyTools.LOG(this, "OnEnable");

    }

    void OnDisable()
    {
        MyTools.LOG(this, "OnDisable");
    }

    private void OnDestroy()
    {
        MyTools.LOG(this, "OnDestroy");
    }

    // Start is called before the first frame update
    void Start()
    {
        MyTools.LOG(this, "Start");
    }

    // Update is called once per frame
    void Update()
    {
        MyTools.LOG(this, "Update");
    }

    private void FixedUpdate()
    {
        MyTools.LOG(this, "FixedUpdate");
    }

    private void LateUpdate()
    {
        MyTools.LOG(this, "LateUpdate");
    }
}
