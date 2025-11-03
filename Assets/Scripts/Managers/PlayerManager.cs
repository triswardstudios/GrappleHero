using UnityEngine;

public class PlayerManager : MonoBehaviour,IDamageble
{
    [SerializeField] GameEvent OngameOverEvent;

    public void TakeDamage(float amount)
    {
        OngameOverEvent.Raise(this, true);
    }
}
