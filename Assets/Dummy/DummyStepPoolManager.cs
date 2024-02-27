﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyStepPoolManager : MonoBehaviour
{
    #region Instance

    private static DummyStepPoolManager instance;

    public static DummyStepPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DummyStepPoolManager();
                return instance;
            }

            return instance;
        }
    }

    #endregion

    [Header("Steps")]
    [SerializeField]
    private GameObject leftStep;
    [SerializeField]
    private GameObject rightStep;

    [Header("Player")]
    [SerializeField]
    private Transform playerTr;
    private Queue<DummyStep> stepQueue = new Queue<DummyStep>();
    private int stepIndex = 0;

    [Header("Enemy")]
    [SerializeField]
    private List<DummyEnemy> enemies = new List<DummyEnemy>();
    [SerializeField]
    private Transform enemyTr;

    private Queue<DummyLeftStep> enemyLeftStepQueue = new Queue<DummyLeftStep>();
    private Queue<DummyRightStep> enemRightStepQueue = new Queue<DummyRightStep>();

    private void Awake()
    {
        if (Instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        Initialize(10);
        EnemyInitialize(10 * enemies.Count);
        playerTr = FindFirstObjectByType<DummyPlayerController>().GetComponent<Transform>();
    }

    #region Player

    private void Initialize(int initCount)
    {
        for (int i = 0; i < initCount; i++)
            stepQueue.Enqueue(CreateNewObject());
    }

    private DummyStep CreateNewObject()
    {
        DummyStep step = null;

        if (stepIndex == 0)
        {
            step = Instantiate(leftStep).GetComponent<DummyStep>();
            stepIndex = -1;
        }
        else
        {
            step = Instantiate(rightStep).GetComponent<DummyStep>();
            stepIndex = 0;
        }

        step.stepType = StepType.Player;
        step.SetMaterial(step.ren.material);
        step.StepDisable();
        step.transform.SetParent(this.transform);

        return step;
    }

    public DummyStep GetStep()
    {
        Vector3 pos = playerTr.position;
        Quaternion rot = playerTr.rotation;

        if (stepQueue.Count > 0)
        {
            var step = stepQueue.Dequeue();
            step.transform.SetParent(null);
            step.transform.position = new Vector3(pos.x, 0, pos.z);
            step.transform.rotation = rot;

            step.gameObject.SetActive(true);

            step.OnStep();

            return step;
        }
        else
        {
            var newStep = CreateNewObject();

            newStep.gameObject.SetActive(true);
            newStep.transform.SetParent(null);
            newStep.transform.position = new Vector3(pos.x, 0, pos.z);
            newStep.transform.rotation = rot;

            newStep.OnStep();

            return newStep;
        }
    }

    public void ReturnStep(DummyStep step)
    {
        step.transform.SetParent(this.transform);
        stepQueue.Enqueue(step);
    }

    #endregion

    #region Enemy

    private void EnemyInitialize(int initCount)
    {
        for (int i = 0; i < initCount / 2; i++)
            enemyLeftStepQueue.Enqueue(CreateEnemyLeftStep());

        for (int i = 0; i < initCount / 2; i++)
            enemRightStepQueue.Enqueue(CreateEnemyRightStep());
    }

    private DummyLeftStep CreateEnemyLeftStep()
    {
        DummyLeftStep step = null;

        step = Instantiate(leftStep).GetComponent<DummyLeftStep>();

        step.stepType = StepType.Enemy;
        step.SetMaterial(step.ren.material);
        step.StepDisable();
        step.transform.SetParent(this.enemyTr);

        return step;
    }

    private DummyRightStep CreateEnemyRightStep()
    {
        DummyRightStep step = null;

        step = Instantiate(rightStep).GetComponent<DummyRightStep>();

        step.stepType = StepType.Enemy;
        step.SetMaterial(step.ren.material);
        step.StepDisable();
        step.transform.SetParent(this.enemyTr);

        return step;
    }

    public DummyLeftStep GetEnemyLeftStep(Transform tr)
    {
        Vector3 pos = tr.position;
        Quaternion rot = tr.rotation;

        DummyLeftStep step = null;

        if (enemyLeftStepQueue.Count > 0)
        {
            DummyLeftStep leftStep = enemyLeftStepQueue.Dequeue();
            leftStep.transform.SetParent(null);
            leftStep.transform.position = new Vector3(pos.x, 0, pos.z);
            leftStep.transform.rotation = rot;

            leftStep.gameObject.SetActive(true);
            leftStep.OnStep();

            step = leftStep;
        }
        else
        {
            var newStep = CreateEnemyLeftStep();

            newStep.gameObject.SetActive(true);
            newStep.transform.SetParent(null);
            newStep.transform.position = new Vector3(pos.x, 0, pos.z);
            newStep.transform.rotation = rot;

            newStep.OnStep();

            step = newStep;
        }

        return step;
    }

    public DummyRightStep GetEnemyRightStep(Transform tr)
    {
        Vector3 pos = tr.position;
        Quaternion rot = tr.rotation;

        DummyRightStep step = null;

        if (enemRightStepQueue.Count > 0)
        {
            DummyRightStep leftStep = enemRightStepQueue.Dequeue();
            leftStep.transform.SetParent(null);
            leftStep.transform.position = new Vector3(pos.x, 0, pos.z);
            leftStep.transform.rotation = rot;

            leftStep.gameObject.SetActive(true);
            leftStep.OnStep();

            step = leftStep;
        }
        else
        {
            var newStep = CreateEnemyRightStep();

            newStep.gameObject.SetActive(true);
            newStep.transform.SetParent(null);
            newStep.transform.position = new Vector3(pos.x, 0, pos.z);
            newStep.transform.rotation = rot;

            newStep.OnStep();

            step = newStep;
        }

        return step;
    }

    public void ReturnEnemyLeftStep(DummyLeftStep step)
    {
        step.transform.SetParent(this.enemyTr);
        enemyLeftStepQueue.Enqueue(step);
    }

    public void ReturnEnemyRightStep(DummyRightStep step)
    {
        step.transform.SetParent(this.enemyTr);
        enemRightStepQueue.Enqueue(step);
    }

    #endregion
}