using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisionBehavior : MonoBehaviour
{
    public PlayerStats playerStats;
    
    public UI_Stats uI_Stats;

    public PlayerController playerController;

    public static PlayerCollisionBehavior Instance;

    private Rigidbody rb;
    private MeshRenderer meshRenderer;

    private int nbrOfFlashing = 3;

    private TrailRenderer trailRenderer;

    public string Player_Fall;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        trailRenderer = GetComponent<TrailRenderer>();

        playerController = GetComponent<PlayerController>();
        
        playerStats = GetComponent<PlayerStats>();

        rb = GetComponent<Rigidbody>();

        meshRenderer = GetComponent<MeshRenderer>();

    }

    private void Start()
    {
        uI_Stats = UI_Stats.Instance;
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {

            if (collision.gameObject.TryGetComponent(out HoleForPlayer holeForPlayer))
            {
                playerController.ThrowStrength = 0;
                StartCoroutine(HolePlayerScale());
                AudioManager2.Instance.PlaySDFX(Player_Fall);
                rb.velocity = Vector3.zero;
                trailRenderer.enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out MoneyStats moneyStats))
        {
            AddMoney(moneyStats.value);
            uI_Stats.UpdateStats();
            other.gameObject.TryGetComponent(out LootAnimation lootAnimation);
            lootAnimation.StartAnimation();
        }

        if (other.gameObject.TryGetComponent(out CollectibleReloadBoost collectibleReloadBoost) && TurnBasedPlayer.Instance.shotRemaining < TurnBasedPlayer.Instance.nbrOfShots) 
        {
            TurnBasedPlayer.Instance.RecupBoostReload();
            other.gameObject.TryGetComponent(out LootAnimation lootAnimation);
            lootAnimation.StartAnimation();
        }

        if (other.gameObject.TryGetComponent(out EndLevel endLevel))
        {
            SceneManager.LoadScene(endLevel.nextLevel);
        }
        
    }
    public void AddMoney(int money)
    {
        playerStats.moneyCount += money;
    }

    IEnumerator HolePlayerScale()
    {
        InputHandler.Actions.Gamepad.ThrowPlayer.Disable();
        InputHandler.Actions.Gamepad.GamepadStrenght.Disable();
        InputHandler.Actions.MouseKeyboard.MouseStartDrag.Disable();

        Vector3 startSize = transform.localScale;
        Vector3 endSize = new Vector3(0, 0, 0);

        float timerSize = 0f;
        float timeToScale = 0.5f;

        do
        {
            rb.velocity = Vector3.zero;
            transform.localScale = Vector3.Lerp(startSize, endSize, timerSize / timeToScale);
            timerSize += Time.deltaTime;
            yield return null;
        }
        while (timerSize < timeToScale);

        transform.position = playerController.posBeforeHit;
        rb.velocity = Vector3.zero;
        trailRenderer.enabled = true;
        trailRenderer.Clear();
        transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(HoleFeedBack());
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
        nbrOfFlashing = 2;
        InputHandler.Actions.Gamepad.ThrowPlayer.Enable();
        InputHandler.Actions.Gamepad.GamepadStrenght.Enable();
        InputHandler.Actions.MouseKeyboard.MouseStartDrag.Enable();
    }
}
