using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class TestSuite
{
    private GameObject game;
    private GameManager gameManager;
    private Player player;

    [SetUp]
    public void Setup()
    {
        // Load and spawn Game prefab
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(prefab);
        // Get GameManager
        gameManager = GameManager.Instance;
        //player = Object.FindObjectOfType<Player>();
        player = game.GetComponentInChildren<Player>();
    }

    [UnityTest]
    public IEnumerator GamePrefabLoaded()
    {
        yield return new WaitForEndOfFrame();

        // Game object should exist at this point in time
        Assert.NotNull(game);
    }

    [UnityTest]
    public IEnumerator PlayerExists()
    {
        yield return new WaitForEndOfFrame();

        Assert.NotNull(player, "Where is da player?? You gotta assign da player ");
    }

    [UnityTest]
    public IEnumerator ItemCollidesWithPlayer()
    {
        //Item item = gameManager.itemManager.GetItem(0);
        //item.transform.position = player.transform.position;

        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");

        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, playerPosition, Quaternion.identity);        

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(item == null);
    }

    [UnityTest]
    public IEnumerator ItemCollectedAndScoreAdded()
    {
        // Spawn an item (same as above)
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, playerPosition, Quaternion.identity);
        bool differentScore = false;
        // Record old score in an int
        int oldScore = gameManager.score;
        Debug.Log("Old Score: " + oldScore);
        gameManager.AddScore(gameManager.score);
        int newScore = gameManager.score;
        Debug.Log("New Score: " + newScore);

        if (newScore != oldScore)
        {
            differentScore = true;
        }

        // WaitForFixedUpdate
        yield return new WaitForFixedUpdate();
        // WaitForEndOffFrame
        yield return new WaitForEndOfFrame();
        // Assert IsTrue old score != new score
        Assert.IsTrue(differentScore);
    }

    [UnityTest]
    public IEnumerator PlayerJump()
    {
        yield return new WaitForEndOfFrame();
        float playerPositionY = player.transform.localPosition.y;
        player.Jump();
        yield return new WaitForEndOfFrame();
        Assert.Greater(player.transform.localPosition.y, playerPositionY);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}
