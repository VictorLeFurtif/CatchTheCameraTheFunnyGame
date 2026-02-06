using UnityEngine;

public class CollectableAlcool : AbstractCollectable
{
    protected override void Collect()
    {
        EventManager.AlcoolEffect();
        base.Collect();
    }
}
