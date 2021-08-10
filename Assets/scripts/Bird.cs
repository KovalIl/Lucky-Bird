using System;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private const float JUMP_AMOUNT = 80f;
    private Rigidbody2D _rigidbody;
    private static Bird instance;
    private State state;

    public static Bird GetInstance()
    {
        return instance;
    }
    public EventHandler OnDied;
    public EventHandler OnStartedPlaying;

    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }

    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.bodyType = RigidbodyType2D.Static;
        GetComponent<Animator>().speed = 0.1f;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    state = State.Playing;
                    _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    Jump();
                }
                transform.eulerAngles = new Vector3(0f, 0f, _rigidbody.velocity.y * .2f);
                break;
            case State.Dead:
                break;
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = Vector2.up * JUMP_AMOUNT;
        SoundManager.PlaySound(SoundManager.Sound.BirdJump);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _rigidbody.bodyType = RigidbodyType2D.Static;
        SoundManager.PlaySound(SoundManager.Sound.Lose);
        if (OnDied != null)
        {
            OnDied(this, EventArgs.Empty);
        }
    }
}