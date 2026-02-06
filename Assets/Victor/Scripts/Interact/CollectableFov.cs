using UnityEngine;

public class CollectableFov : AbstractCollectable
{
    protected override void Collect()
    {
        EventManager.FovEffect();
        base.Collect();
    }
}
