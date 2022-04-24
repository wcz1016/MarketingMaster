using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolkGenerator : MonoBehaviour
{
    public GameObject FolkPrefab;
    public Transform UpperSpawn, LowerSpawn;
    public int FolkLimit;
    public int FolkBatch;
    public int PathPointCount;
    public float GenerationInterval;
    public float FolkIdleSpeed;

    private float _generationTimer = 0;
    public Transform LeftShop, RightShop;

    // Update is called once per frame
    void Update()
    {
        _generationTimer += Time.deltaTime;
        if (_generationTimer > GenerationInterval && Folk.ActiveFolksCount < FolkLimit)
        {
            GenerateFolk(UpperSpawn, LowerSpawn);
            _generationTimer = 0;
        }
    }

    void GenerateFolk(Transform spawn, Transform destination)
    {
        float spawnLowerX = spawn.position.x - spawn.localScale.x / 2;
        float spawnUpperX = spawn.position.x + spawn.localScale.x / 2;
        float spawnX = Random.Range(spawnLowerX, spawnUpperX);
        GameObject spwanedFolk = Instantiate(FolkPrefab, new Vector2(spawnX, spawn.position.y), Quaternion.identity);

        float destLowerX = destination.position.x - destination.localScale.x / 2;
        float destUpperX = destination.position.x + destination.localScale.x / 2;

        List<Vector2> targets = new List<Vector2>();

        float height = UpperSpawn.position.y - LowerSpawn.position.y;
        float targetYRange = height;
        float XDiff = LowerSpawn.localScale.x - UpperSpawn.localScale.x;


        for (int j = 0; j < PathPointCount; j++)
        {
            float targetYDistance = Random.Range(0, targetYRange);
            targetYRange = targetYDistance;
            float targetY = LowerSpawn.position.y + targetYDistance;

            float targetXLength = (targetY - UpperSpawn.position.y) * XDiff / height + UpperSpawn.position.x;
            float targetLowerX = spawn.position.x - targetXLength / 2;
            float targetUpperX = spawn.position.x + targetXLength / 2;

            targets.Add(new Vector2(Random.Range(targetLowerX, targetUpperX), targetY));
        }


        targets.Add(new Vector2(Random.Range(destLowerX, destUpperX), destination.position.y));

        Folk folk = spwanedFolk.GetComponent<Folk>();
        folk.SetPath(targets);
        folk.IdleSpeed = FolkIdleSpeed;
        folk.LeftShop = LeftShop;
        folk.RightShop = RightShop;

    }
}
