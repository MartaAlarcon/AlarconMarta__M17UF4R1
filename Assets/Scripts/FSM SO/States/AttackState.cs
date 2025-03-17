using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackState", menuName = "StatesSO/Attack")]
public class AttackState : StateSO
{
    public override void OnStateEnter(EnemyController ec)
    {
    }

    public override void OnStateExit(EnemyController ec)
    {
    }

    public override void OnStateUpdate(EnemyController ec)
    {
        GameManager.gm.UpdateText("Te reviento a chancletaso");
    }
}
