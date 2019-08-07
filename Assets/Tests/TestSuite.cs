﻿using System.Collections;
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

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}
