using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] int amount = 1;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Collison Wit player");
            GameAssets.Instance.OnCoinCollectedEvent.Raise(this,amount);
            Destroy(this.gameObject);
        }
    }
}
