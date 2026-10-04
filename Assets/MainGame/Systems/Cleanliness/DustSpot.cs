using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DustSpot : MonoBehaviour, IToolTarget
{
    public float Clean { get; private set; }

    public bool Accepts(ToolType tool)
    {
        return tool == ToolType.Cloth && Clean < 1f;
    }

    public void Use(ToolType tool, float amount)
    {
        GetComponentInParent<Cleanliness>().Rub(this, amount);
    }

    public void SetClean(float value)
    {
        Clean = Mathf.Clamp01(value);

        var dust = GetComponent<SpriteRenderer>();
        var color = dust.color;
        color.a = 1f - Clean;
        dust.color = color;
        dust.enabled = Clean < 1f;
    }
}