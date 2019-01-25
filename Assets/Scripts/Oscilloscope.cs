using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscilloscope : MonoBehaviour
{
    public GameObject scopePrefab;
    private Transform scopePen;

    // Start is called before the first frame update
    void Start()
    {
        scopePen = Instantiate(scopePrefab, GetComponent<Transform>()).GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float osc = 2f;
        osc += 0.3f * Mathf.Sin(7f / 12f * Time.time); 
        osc += 0.8f * Mathf.Sin(50f / 12f * Time.time);
        scopePen.position = new Vector3(osc * Mathf.Sin(Time.time), osc * Mathf.Cos(Time.time));
    }

}
