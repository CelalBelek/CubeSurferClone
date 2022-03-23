using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EmeraldManager : MonoBehaviour
{
    public int emeraldCount;
    public TextMeshProUGUI emeraldText;

    public GameObject emeraldPrefab;
    public Transform emeraldTarget;
    public int maxEmeralds;
    Queue<GameObject> emeraldQueue = new Queue<GameObject>();

    [SerializeField] [Range(0.5f, 1.5f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 4.0f)] float maxAnimDuration;
    [SerializeField] Ease easeType;
    public float spread;

    Vector3 targetPosition;


    private void Awake()
    {
        targetPosition = emeraldTarget.position + new Vector3(Random.Range(-spread,spread), 0.0f, 0.0f);

        EmeraldText();
        PreperaCoins();
    }

    void PreperaCoins()
    {
        GameObject emerald;
        for (int i = 0; i < maxEmeralds; i++)
        {
            emerald = Instantiate(emeraldPrefab);
            emerald.transform.parent = transform;
            emerald.SetActive(false);
            emeraldQueue.Enqueue(emerald);
        }
    }

    void Animate(Vector3 collectedEmeraldPosition, int amount)
    {
        Debug.Log("Girdi");
        for (int i = 0; i < amount; i++)
        {
            if (emeraldQueue.Count > 0)
            {
                GameObject emerald = emeraldQueue.Dequeue();
                emerald.SetActive(true);

                emerald.transform.position = collectedEmeraldPosition;

                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                emerald.transform.DOMove(targetPosition, duration)
                    .SetEase(easeType)
                    .OnComplete(() => {
                        emerald.SetActive(false);
                        emeraldQueue.Enqueue(emerald);
                    });
                EmeraldSave();
                EmeraldText();
            }
        }
    }

    public void AddEmearlds(Vector3 collectedEmeraldPosition, int amount)
    {
        Animate(collectedEmeraldPosition, amount);
    }

    public void EmeraldText()
    {
        emeraldText.text = PlayerPrefs.GetInt("EmeraldCount").ToString();
    }

    void EmeraldSave()
    {
        PlayerPrefs.SetInt("EmeraldCount", PlayerPrefs.GetInt("EmeraldCount") + 1);
    }
}
