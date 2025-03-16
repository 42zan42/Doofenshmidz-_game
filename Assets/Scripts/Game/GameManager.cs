using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.Instantiate("Player Manager", Vector3.zero, Quaternion.identity);
    }

}

public interface IDamageable
{
    public void TakeDamage(int damage);
}
