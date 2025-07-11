using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideChecker : MonoBehaviour
{
    public bool IsPlayerInside { get; private set; }
    public event Action PlayerEntered;
    public event Action PlayerLeft;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover _))
            PlayerEntered?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out PlayerMover _))
            PlayerLeft?.Invoke();
    }
}
