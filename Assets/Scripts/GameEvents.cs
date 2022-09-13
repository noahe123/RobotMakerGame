using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<Vector4> onSampleEvent;

    public void SampleEvent(Vector4 id)
    {
        if (onSampleEvent != null)
        {
            onSampleEvent(id);
        }
    }

}
