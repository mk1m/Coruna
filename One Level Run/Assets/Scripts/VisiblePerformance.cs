using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisiblePerformance : MonoBehaviour
{
    void OnBecameInvisible()
    {
        enabled = false;
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
}
