using Photon.Pun;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private int _damage = 50;
    [SerializeField] private Collider _collider;

    private GameObject _objectToIgnore;

    private IEnumerator DestroyAfter(GameObject gameobject, float time)
    {
        yield return new WaitForSeconds(time);
        PhotonNetwork.Destroy(gameObject);
    }

    private void Awake()
    {
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.useGravity = false;
        StartCoroutine(DestroyAfter(gameObject, 1.0f));
    }

    private void Update()
    {

    }

    public void SetDirection(Vector3 direction, GameObject objectToIgnore = null)
    {
        _rb.AddForce(direction.normalized * _speed, ForceMode.Impulse);
        transform.forward = direction;

        _objectToIgnore = objectToIgnore;

        _collider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != _objectToIgnore && other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(_damage);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
