using UnityEngine;
using UnityEngine.Events;

namespace CK {
  public class Health : MonoBehaviour {
    [Tooltip("Maximum amount of health")]
    [SerializeField] float maxHP = 10f;

    [Tooltip("Health ratio at which the critical health vignette starts appearing")]
    [SerializeField] float CriticalHealthRatio = 0.3f;

    public UnityAction<float, GameObject> OnDamaged;
    public UnityAction<float> OnHealed;
    public UnityAction OnDie;

    public float curHP { get; set; }
    public bool Invincible { get; set; }
    public bool CanBeHealed() => curHP < maxHP;

    public float GetRatio() => curHP / maxHP;
    public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

    bool m_IsDead;

    void Start() {
      curHP = maxHP;
    }

    public void Heal(float amount) {
      var preHP = curHP;
      curHP = Mathf.Clamp(curHP + amount, 0f, maxHP);

      // call OnHeal action
      amount = curHP - preHP;
      if (amount > 0f) {
        OnHealed?.Invoke(amount);
      }
    }

    public void TakeDamage(float damage, GameObject damageSource) {
      if (Invincible)
        return;

      float preHP = curHP;
      curHP = Mathf.Clamp(curHP -= damage, 0f, maxHP);

      float amount = preHP - curHP;
      if (amount > 0f) {
        OnDamaged?.Invoke(amount, damageSource);
      }

      HandleDeath();
    }

    public void Kill() {
      curHP = 0f;

      // call OnDamage action
      OnDamaged?.Invoke(maxHP, null);

      HandleDeath();
    }

    void HandleDeath() {
      if (m_IsDead)
        return;

      // call OnDie action
      if (curHP <= 0f) {
        m_IsDead = true;
        OnDie?.Invoke();
      }
    }
  }
}