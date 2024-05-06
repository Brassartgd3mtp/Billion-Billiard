using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform playerTransform; // Référence au transform du joueur
    public bool LookPlayer ; 
    void Update()
    {
        if (LookPlayer == true)
            { 
        if (playerTransform != null)
        {
            // Tourner l'objet pour qu'il regarde vers le joueur
            transform.LookAt(playerTransform);
        }
        }
    }
}
