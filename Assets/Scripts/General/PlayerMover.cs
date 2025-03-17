using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        _rb.velocity = 5f * new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
    }
}
