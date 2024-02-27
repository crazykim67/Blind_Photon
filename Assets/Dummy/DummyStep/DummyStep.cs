using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DummyStepPos
{
    Left,
    Right,
}

public enum DummyStepType
{
    Player,
    Enemy,
}

public abstract class DummyStep : MonoBehaviour
{
    public StepPos stepPos = StepPos.Left;

    public StepType stepType;

    public SpriteRenderer ren;
    public Material mat;
    public Light spotLight;

    protected float alpha = 0f;
    protected float intensity = 0f;

    public float showSpeed = 0.2f;
    public float hideSpeed = 0.2f;

    public abstract void SetMaterial(Material _mat);
    public abstract void OnStep();
    public abstract void ReturnStep();
    public abstract IEnumerator ShowStep();
    public abstract IEnumerator HideStep();

    public abstract void StepDisable();
}
