using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    protected SkinnedMeshRenderer[] renderer;

    [Header("Flash FX")]
    [SerializeField] protected float flashDuration;
    [SerializeField] protected Material hitMat;
    private List<Material> originalMats;

    public virtual void PlayFlashFX()
    {
        StartCoroutine(FlashFX());
    }

    protected virtual void OnLoadComponents()
    {
        originalMats = new List<Material>();

        for (int i = 0; i < renderer.Length; i++)
        {
            originalMats.Add(renderer[i].material);
        }
    }

    private IEnumerator FlashFX()
    {
        renderer.ToList().ForEach(x => x.material = hitMat);
        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < renderer.Length; i++)
        {
            renderer[i].material = originalMats[i];
        }

    }
}
