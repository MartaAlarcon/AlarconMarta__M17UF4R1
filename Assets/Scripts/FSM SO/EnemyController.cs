using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int HP;
    public GameObject target;
    public int DamagedHP;
    public bool OnRange = false, OnAttackRange = false;
    private ChaseBehaviour _chaseB;
    public StateSO currentNode;
    public List<StateSO> Nodes;

    void Start()
    {
        _chaseB = GetComponent<ChaseBehaviour>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
        OnRange = true;
        CheckEndingConditions();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnRange = false;
        CheckEndingConditions();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnAttackRange = true;
        CheckEndingConditions();
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnAttackRange = false;
        CheckEndingConditions();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            HP--;
            CheckEndingConditions();
        }
        currentNode.OnStateUpdate(this);
    }
    public void CheckEndingConditions()
    {
        foreach (ConditionSO condition in currentNode.EndConditions)
            if (condition.CheckCondition(this) == condition.answer) ExitCurrentNode();
    }
    public void ExitCurrentNode()
    {
        foreach (StateSO stateSO in Nodes)
        {
            if (stateSO.StartCondition == null)
            {
                EnterNewState(stateSO);
                break;
            }
            else
            {
                if (stateSO.StartCondition.CheckCondition(this) == stateSO.StartCondition.answer)
                {
                    EnterNewState(stateSO);
                    break;
                }
            }
        }
        currentNode.OnStateEnter(this);
    }
    private void EnterNewState(StateSO state)
    {
        currentNode.OnStateExit(this);
        currentNode = state;
        currentNode.OnStateEnter(this);
    }
}
