using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, ILifeHandler
{
    [SerializeField] string Tag;
    public static Action<GameObject> OnDeathh;
    [SerializeField] UnityEvent onDeath;
    public event Action OnDamageTaken;
    public event Action OnDeath;
    public event Action<float> onValueUpdate;

    [SerializeField] float maxHealth;
    [SerializeField] float currentHealth;

    VariableContainer Variables { get => VariableManager.ins.GetVariableList(Tag); }
    public float MaxHealth { get => Variables.GetFloat("MaxHealth"); }

    
    public void addLife(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        onValueUpdate?.Invoke(currentHealth);
    }

    public void die()
    {
        onDeath?.Invoke();
        OnDeathh?.Invoke(gameObject);
        GetComponent<PoolObject>().release();
    }

    public float getCurrent()
    {
        return currentHealth;
    }

    public void getDamage(float amount)
    {
        currentHealth = Mathf.Clamp(currentHealth - amount, 0f, MaxHealth);
        OnDamageTaken?.Invoke();
        if (currentHealth == 0)
            die();
    }

    public float getMax()
    {
        return MaxHealth;
    }

    public float getPercent()
    {
        return currentHealth / MaxHealth;
    }

    public bool tagCheck(string tag)
    {
        return false;
    }


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }
}
