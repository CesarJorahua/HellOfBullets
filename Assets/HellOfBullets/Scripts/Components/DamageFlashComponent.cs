using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class DamageFlashComponent: MonoBehaviour
{
    private static WaitForSeconds _waitForSeconds1 = new WaitForSeconds(1f);
    private MeshRenderer _renderer;

    public virtual void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void StartFlashEffect()
    {
        StartCoroutine(OnDamageTake());
    }

    private IEnumerator OnDamageTake()
    {
        _renderer.material.SetFloat("_DamageFlashAmount",10f);
        yield return _waitForSeconds1;
        StopFlashEffect();
    }

    public void StopFlashEffect()
    {
        _renderer.material.SetFloat("_DamageFlashAmount",0f);
    }

    private void OnDestroy()
    {
        //StopAllCoroutines();
    }
}
