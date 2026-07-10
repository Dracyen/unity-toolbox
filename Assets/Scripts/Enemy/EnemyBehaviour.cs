using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : EnemyEntity
{
    [Space, Header("Movement Values")]

    public bool Updating;

    public Vector3 Direction;

    public bool hasDirection;

    [Range(1, 10)]
    public float Speed = 1;

    [Space, Header("Timer Values")]

    public float timer = 0;

    [Range(1, 10)]
    public float timerMax = 1;

    [Space, Header("References")]

    public GameObject Explosion;

    private void Awake()
    {
        TimeController.Instance.OnTimeResume += ResumeUpdate;
        TimeController.Instance.OnTimePause += PauseUpdate;

        Health = 100;

        Updating = true;
        hasDirection = false;

        timer = 0;
    }

    void Update()
    {
        if(Updating)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (hasDirection)
            timer += UnityEngine.Time.deltaTime;

        if (timer > timerMax)
            hasDirection = false;

        if (!hasDirection)
        {
            hasDirection = true;

            timer = 0;

            Direction = new Vector3(UnityEngine.Random.Range(-1.0f, 1.0f), 0, UnityEngine.Random.Range(-1.0f, 1.0f));
        }

        transform.position += Direction * Speed * UnityEngine.Time.deltaTime;
    }

    void PauseUpdate()
    {
        Updating = false;
    }

    void ResumeUpdate()
    {
        Updating = true;
    }

    public override void DamageHealth(int damage)
    {
        Health -= damage;

        if (Health <= 0) Kill();
    }

    public override void Kill()
    {
        Explosion.transform.parent = null;

        Updating = false;

        foreach(ParticleSystem vfx in Explosion.GetComponentsInChildren<ParticleSystem>())
        {
            vfx.Play();
        }

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        TimeController.Instance.OnTimeResume -= ResumeUpdate;
        TimeController.Instance.OnTimePause -= PauseUpdate;
    }
}
