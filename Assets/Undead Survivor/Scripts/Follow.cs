using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (!GameManager.instace.isLive)
            return;

        rect.position = Camera.main.WorldToScreenPoint(GameManager.instace.player.transform.position);
    }
}
