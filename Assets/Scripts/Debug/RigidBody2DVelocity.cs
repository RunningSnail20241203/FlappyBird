using System;
using UnityEngine;

public class RigidBody2DVelocity : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody2D;
    [SerializeField] private RectTransform showVelocity;
    [SerializeField] private float velocityMultiplier = 0.5f;

    private void Update()
    {
        showVelocity.pivot = new Vector2(0.5f, rigidBody2D.velocity.y > 0 ? 0f : 1f);
        showVelocity.anchoredPosition = new Vector2(showVelocity.anchoredPosition.x, 0);
        showVelocity.sizeDelta = new Vector2(50, Mathf.Abs(rigidBody2D.velocity.y * velocityMultiplier));
    }
}