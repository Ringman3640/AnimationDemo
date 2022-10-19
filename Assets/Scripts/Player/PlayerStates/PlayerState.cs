using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class PlayerState
{
    public bool completed
    {
        get;
        protected set;
    }

    protected GameObject player;
    protected bool started;
    protected float startTime;

    public PlayerState()
    {
        player = GameObject.Find("Player");
        Assert.IsNotNull(player);

        started = false;
        completed = false;
        startTime = -1f;
    }

    public void Execute()
    {
        if (startTime < 0f)
        {
            startTime = Time.time;
        }

        if (!completed)
        {
            ActionExecution();
        }
    }

    public void Completed()
    {
        completed = true;
    }

    protected abstract void ActionExecution();
}
