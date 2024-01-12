using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionBehavior : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public UI_Stats uI_Stats;

    public PlayerController playerController;

    public static PlayerCollisionBehavior Instance;

    private Rigidbody rb;
    private MeshRenderer meshRenderer;

    private int nbrOfFlashing = 3;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        playerController = GetComponent<PlayerController>();
        
        playerStats = GetComponent<PlayerStats>();

        rb = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();

        uI_Stats = UI_Stats.Instance;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            if (collision.gameObject.TryGetComponent(out MoneyStats moneyStats))
            {
                AddMoney(moneyStats.value);
                uI_Stats.UpdateStats();
                collision.gameObject.TryGetComponent(out LootAnimation lootAnimation);
                lootAnimation.StartAnimation();
            }

            if (collision.gameObject.TryGetComponent(out HoleForPlayer holeForPlayer))
            {
                rb.velocity = Vector3.zero;
                transform.position = playerController.posBeforeHit;
                StartCoroutine(HoleFeedBack());
            }
        }
    }

    public void AddMoney(int money)
    {
        playerStats.moneyCount += money;
    }

    IEnumerator HoleFeedBack()
    {
        while (nbrOfFlashing > 0) 
        {
            meshRenderer.material.color = Color.white;
            yield return new WaitForSeconds(0.15f);
            meshRenderer.material.color = Color.black;
            yield return new WaitForSeconds(0.15f);
            nbrOfFlashing--;
        }

        meshRenderer.material.color = Color.white;
    }
}
