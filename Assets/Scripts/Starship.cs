using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starship : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(igniteEngine(15));
    }

    IEnumerator igniteEngine(float bonus_time)
    {
        yield return new WaitForSeconds(bonus_time);
        Debug.Log("nowww");
        for (int i = 0; i < 1000000; i++)
        {
            m_Rigidbody.AddForce(transform.up * 15, ForceMode.Force);
            yield return new WaitForSeconds(0.01f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
