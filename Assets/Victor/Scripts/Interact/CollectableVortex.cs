using UnityEngine;

public class CollectableVortex : AbstractCollectable
{
    protected override void Collect()
    {
        EventManager.VortexEffect();
        base.Collect();
    }
}
