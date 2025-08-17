using System.Collections;
using UnityEngine;
using System;

public class Cash : DroppableCurreny
{
    [Header("Actions")]
    public static Action<Cash> onCollected;
   
    protected override void Collectted()
    {
        onCollected?.Invoke(this);
    }
}
