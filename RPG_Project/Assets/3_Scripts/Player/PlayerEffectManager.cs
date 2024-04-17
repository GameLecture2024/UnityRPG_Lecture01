using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectManager : EntityFX
{
    private void Awake()
    {
        renderer = GetComponentsInChildren<SkinnedMeshRenderer>();     
        OnLoadComponents();
    }

    public override void PlayFlashFX()
    {
        base.PlayFlashFX();
    }
}
