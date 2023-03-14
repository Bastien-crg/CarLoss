using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour,IDestroyable
{
    public void Kill()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

}
