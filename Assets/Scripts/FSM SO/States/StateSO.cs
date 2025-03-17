using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateSO : ScriptableObject
{
    public ConditionSO StartCondition;
    public List<ConditionSO> EndConditions;
    public abstract void OnStateEnter(EnemyController ec);
    public abstract void OnStateUpdate(EnemyController ec);
    public abstract void OnStateExit(EnemyController ec);

}
