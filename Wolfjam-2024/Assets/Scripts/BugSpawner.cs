using UnityEngine;
using System.Collections;

public class BugSpawner : MonoBehaviour
{
    public GameObject bugPrefab;         // Reference to the bug prefab
    public float spawnInterval = 10f;     // Time interval between bug spawns
    public float growthDuration = 10f;    // Duration for the bug to grow to normal size
    private Vector3 screenBottomLeft;
    private Vector3 screenTopRight;
    private Vector3 screenCenter;
    public float spawnOffset = 5f; // Distance outside the screen bounds to spawn bugs


    void Start()
    {
        // Calculate screen bounds once at the start
        screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        // Calculate the center of the screen in world coordinates
        screenCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

        // Start spawning bugs at intervals
        InvokeRepeating(nameof(SpawnBug), 0f, spawnInterval);
    }

    void SpawnBug()
    {
        // Determine a random side of the screen to spawn (top, bottom, left, right)
        int side = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0: // Top
                spawnPosition = new Vector3(
                    Random.Range(screenBottomLeft.x, screenTopRight.x),
                    screenTopRight.y + spawnOffset,
                    -5f
                );
                break;
            case 1: // Bottom
                spawnPosition = new Vector3(
                    Random.Range(screenBottomLeft.x, screenTopRight.x),
                    screenBottomLeft.y - spawnOffset,
                    -5f
                );
                break;
            case 2: // Left
                spawnPosition = new Vector3(
                    screenBottomLeft.x - spawnOffset,
                    Random.Range(screenBottomLeft.y, screenTopRight.y),
                    -5f
                );
                break;
            case 3: // Right
                spawnPosition = new Vector3(
                    screenTopRight.x + spawnOffset,
                    Random.Range(screenBottomLeft.y, screenTopRight.y),
                    -5f
                );
                break;
        }

        // Instantiate the bug at the calculated position
        GameObject newBug = Instantiate(bugPrefab, spawnPosition, Quaternion.identity);

        // Start the grow animation
        StartCoroutine(GrowBug(newBug));
    }

    private IEnumerator GrowBug(GameObject bug)
    {
        // Set the initial scale to zero
        bug.transform.localScale = Vector3.zero;

        float elapsedTime = 0f;

        // Gradually grow the bug to full size
        while (elapsedTime < growthDuration)
        {
            float growthFactor = Mathf.Lerp(0f, 1f, elapsedTime / growthDuration);
            bug.transform.localScale = new Vector3(5f * growthFactor, 5f * growthFactor, 1f);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        // Ensure the bug ends at full size
        bug.transform.localScale = new Vector3(5f, 5f, 1f);
    }
}
