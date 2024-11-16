using UnityEngine;
using System.Collections;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPrefab;         // Reference to the bug prefab
    public float spawnInterval = 10f;     // Time interval between bug spawns
    public Vector2 spawnAreaMin;         // Minimum x and y coordinates for spawn area
    public Vector2 spawnAreaMax;         // Maximum x and y coordinates for spawn area
    public float growthDuration = 10f;    // Duration for the bug to grow to normal size

    void Start()
    {
        // Start spawning bugs at regular intervals
        StartCoroutine(SpawnBug());
    }

    private IEnumerator SpawnBug()
    {
        while (true)
        {
            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Random position within the spawn area
            float spawnX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float spawnY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

            // Instantiate the bug prefab at the random position
            GameObject bug = Instantiate(bugPrefab, spawnPosition, Quaternion.identity);

            // Start the growth animation for the bug
            StartCoroutine(GrowBug(bug));

            // Optional: Delay the spawn rate for next bug
        }
    }

    private IEnumerator GrowBug(GameObject bug)
    {
        // Initially set the bug to a small size
        bug.transform.localScale = Vector3.zero;

        float elapsedTime = 0f;

        bug.transform.position = new Vector3(bug.transform.position.x, bug.transform.position.y, -5f);

        // Gradually grow the bug to 5 by 5 size
        while (elapsedTime < growthDuration)
        {
            float growthFactor = Mathf.Lerp(0f, 1f, elapsedTime / growthDuration);
            bug.transform.localScale = new Vector3(5f * growthFactor, 5f * growthFactor, 1f);

            elapsedTime += Time.deltaTime;
            yield return null;  // Wait for the next frame
        }

        // Ensure the bug ends up at the full size of 5 by 5
        bug.transform.localScale = new Vector3(5f, 5f, 1f);
    }
}
