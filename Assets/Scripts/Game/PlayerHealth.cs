using Photon.Pun;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    private int Health { get { return _health; } set { _health = value; _healthBar.OnHealthChanged(_health); } }

    private UIPlayerHealthBar _healthBar;
    private PhotonView _photonView;

    public void TakeDamage(int damage)
    {
        _photonView.RPC("RemoutDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    private void RemoutDamage(int damage)
    {
        if(damage <= 0) return;

        Health -= damage;
    }

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();

        _healthBar = GetComponentInChildren<UIPlayerHealthBar>();
        _healthBar.SetMaxHealth(_maxHealth);
        Health = _maxHealth;
    }

}
