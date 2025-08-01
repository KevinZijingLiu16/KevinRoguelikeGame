﻿using UnityEngine;

public class TestingColorTweening : MonoBehaviour
{
    // Use this for initialization
    private void Start()
    {
        LeanTween.value(gameObject, Color.red, Color.green, 1f)
                .setOnUpdate(OnTweenUpdate)
                .setOnUpdateParam(new object[] { "" + 2 });
    }

    private void OnTweenUpdate(Color update, object obj)
    {
        object[] objArr = obj as object[];
        Debug.Log("update:" + update + " obj:" + objArr[0]);
    }
}