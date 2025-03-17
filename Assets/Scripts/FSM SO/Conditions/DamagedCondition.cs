using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DamagedCondition", menuName = "ConditionSO/Damaged")]
public class DamagedCondition : ConditionSO
{
    public override bool CheckCondition(EnemyController ec)
    {
        return ec.HP <= ec.DamagedHP;
    }
}
