using UnityEngine;

public class EffectEarnMoney : MonoBehaviour, IEffect
{
    public int Value;
    public string EffectTag;
    public string _EffectTag => EffectTag;

    public void doEffect(GameObject target)
    {
        /*for (int i = 0; i < Value; i++)
        {
            UIMain.FlyMoneyWithWorldPosition(target.transform.position + Random.insideUnitSphere, 10,
                GameManager.Instance.MoneyAnimationCurve, GameManager.Instance.increaseMoney);
        }*/
    }
}