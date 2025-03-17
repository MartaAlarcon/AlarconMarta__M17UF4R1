using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class ConditionSO : ScriptableObject
{
    public abstract bool CheckCondition(EnemyController ec);
    public bool answer;

}
