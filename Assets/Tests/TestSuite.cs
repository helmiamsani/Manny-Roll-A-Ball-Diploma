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

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}
