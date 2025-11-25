using System.Collections;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject Player;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private bool collided = false;
    public GameObject blade;
    public GameObject replacementPrefab;
    public GrapplePointManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Player = GameObject.Find("Player");
        initialPosition = Player.transform.position;
        initialRotation = Player.transform.rotation;
    }

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided = true;
        if (collision != null && collided) {
            collided = false;
                StartCoroutine(RespawnRoutine(collision));
        }
        IDamageble damageble = collision.collider.GetComponentInParent<IDamageble>();
        if (damageble != null)
        {
            damageble.TakeDamage(1);
        }
    }
    private IEnumerator RespawnRoutine(Collision2D collision)
    {
        // Remember original parent and transform
        Transform parent = Player.transform.parent;
        Vector3 position = Player.transform.position;
        Quaternion rotation = Player.transform.rotation;

        // Destroy the old object
        if (Player != null)
        {
            Destroy(Player);
        }
        
        // Instantiate prefab under same parent in hierarchy
        if (replacementPrefab != null)
        {
            yield return new WaitForSeconds(1f);
            if (Player == null)
            {
                GameObject newObj = Instantiate(replacementPrefab, position, rotation, parent);

                Player = newObj;
                Debug.Log($"Replaced {Player.name} with {newObj.name} under parent {parent?.name ?? "Scene Root"}");
                gm = FindFirstObjectByType<GrapplePointManager>();
                gm.RefreshPoints();
            }
        }
        else
        {
            Debug.LogWarning("No replacement prefab assigned!");
        }

    }

   
}
