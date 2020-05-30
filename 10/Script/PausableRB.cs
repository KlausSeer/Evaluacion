using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausableRB : MonoBehaviour
{
    [SerializeField()]
    private Rigidbody2D _rigidBody;
    void Awake()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();
    }

    private bool paused;
    private Vector2 _pausedVelocity;

    public void Pause()
    {
        _pausedVelocity = _rigidBody.velocity;
        _rigidBody.isKinematic = true;
        paused = true;
    }

    public void Resume()
    {
        _rigidBody.isKinematic = false;
        _rigidBody.velocity = _pausedVelocity;
        paused = false;
    }

    public bool isPaused()
    {
        return paused;
    }
}
