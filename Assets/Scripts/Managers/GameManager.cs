using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject replacementPrefab;
    [SerializeField]private GameObject Player;

    void Start()
    {
        
    }

    public void OnGameOver()
    {
        StartCoroutine(RespawnRoutine());
    }


    private IEnumerator RespawnRoutine()
    {
        // Remember original parent and transform
        Transform parent = Player.transform.parent;
        Vector3 position = Player.transform.position;
        Quaternion rotation = Player.transform.rotation;

        // Destroy the old object
        Destroy(Player);

        // Instantiate prefab under same parent in hierarchy
        if (replacementPrefab != null)
        {
            yield return new WaitForSeconds(1f);
            GameObject newObj = Instantiate(replacementPrefab, position, rotation, parent);
            Player = newObj;
        }
        else
        {
            Debug.LogWarning("No replacement prefab assigned!");
        }

    }
    


    public void ListenToOnGameOver(Component sender,object data)
    {
        OnGameOver();
    }
}
