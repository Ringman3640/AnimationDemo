using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAnimation : MonoBehaviour
{
    public enum MoveState
    {
        Idle,
        Running
    };

    public enum LookState
    {
        Front,
        StraightFront,
        Back,
        StraightBack
    };

    public static MoveState moveState
    {
        get;
        set;
    }

    public static LookState lookState
    {
        get;
        set;
    }

    public static bool flipX
    {
        get;
        set;
    }

    private static Animator anim;
    private static SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Assert.IsNotNull(anim);

        sr = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(sr);
    }

    public static void UpdateAnimation()
    {
        switch (lookState)
        {
            case LookState.Front:
                PlayAnimation("front");
                return;

            case LookState.StraightFront:
                PlayAnimation("straightfront");
                return;

            case LookState.Back:
                PlayAnimation("back");
                return;

            case LookState.StraightBack:
                PlayAnimation("straightback");
                return;
        }
    }

    private static void PlayAnimation(string lookDirection)
    {
        string animName = "player_" + lookDirection + "_";

        switch (moveState)
        {
            case MoveState.Idle:
                animName += "idle";
                break;

            case MoveState.Running:
                animName += "running";
                break;
        }

        sr.flipX = flipX;
        anim.Play(animName);
    }
}
