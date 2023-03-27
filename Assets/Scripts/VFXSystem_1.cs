using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class VFXSystem
{
    private readonly float timeToDissolve;
    private readonly PoolerGO poolerGO;

    private List<SpriteRenderer> spriteRenderers= new List<SpriteRenderer>();
    private Material[] materialsDessolved;
    public VFXSystem(float timeToDissolve, PoolerGO poolerGO)
    {
        this.timeToDissolve = timeToDissolve;
        this.poolerGO = poolerGO;
    }
    public void AddRenderer(SpriteRenderer renderer) => spriteRenderers.Add(renderer);
    public void RemoveRenderer(SpriteRenderer renderer) => spriteRenderers?.Remove(renderer);

    public void DessolveAllMaterial()
    {
        SpriteRenderer[] spriteRenderers = FindAllActivSprites();
        materialsDessolved = new Material[spriteRenderers.Length];

        int i = 0;
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            float timeToDissolve1 = timeToDissolve;
            materialsDessolved[i] = spriteRenderer.material;
            LerpMatDessolveValueDawn(spriteRenderer.material, timeToDissolve1, 1);
            i++;
        }    
    }   
    public void ReverseAllMaterial()
    {
        foreach(Material material in materialsDessolved)
        {
            float timeToDissolve1 = timeToDissolve;
            ReverseMaterial(material, 0);
        }
    }
    public async void LerpMatDessolveValueDawn(Material maeterial, float timeToDissolve, int millisecondsAwait)
    {
        while(timeToDissolve >= 0)
        {
            timeToDissolve -= Time.unscaledDeltaTime;
            maeterial.SetFloat("_DessolveValue", timeToDissolve);
            await Task.Delay(TimeSpan.FromMilliseconds(millisecondsAwait));
        }
    }

    private async void ReverseMaterial(Material maeterial, float timeToDissolve)
    {
        while (timeToDissolve <= 1)
        {
            timeToDissolve += Time.unscaledDeltaTime;
            maeterial.SetFloat("_DessolveValue", timeToDissolve);
            await Task.Delay(TimeSpan.FromMilliseconds(1));
        }
    }

    public void CreateVFX(string name, Vector3 pos, Quaternion quaternion)
    {
        var vfx = poolerGO.SpawnFromPool(name, pos, quaternion);
        LerpMatDessolveValueDawn(vfx.GetComponent<SpriteRenderer>().material, 1, 2);
    }

    private SpriteRenderer[] FindAllActivSprites() => spriteRenderers.Where(x => x.gameObject.activeInHierarchy).ToArray();
}
