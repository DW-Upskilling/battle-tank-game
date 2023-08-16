using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    GameObject PauseScreen, PlayerWonScreen, PlayerLostScreen;

    // Following GameObjects will be disabled in the scene
    [SerializeField]
    List<GameObject> AllOtherObjects;

    // Following GameObjects will be destroyed from the scene
    [SerializeField]
    GameObject GroundGameObject, LevelArtGameObject;

    GameManager gameManager;

    protected override void Initialize()
    {
        if (PauseScreen == null || PlayerWonScreen == null || PlayerLostScreen == null || AllOtherObjects == null || GroundGameObject == null || LevelArtGameObject == null)
            throw new MissingReferenceException("Required GameObjects are not available");
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        if(gameManager == null)
            throw new MissingReferenceException("GameManager isn't available");
    }

    public void PlayerWon()
    {

        foreach(GameObject _gameobject in AllOtherObjects){
            _gameobject.SetActive(false);
        }

        PlayerWonScreen.SetActive(true);
    }

    public void PlayerLost(PlayerTankView playerTankView)
    {

        // Remove the TankParts from the Scene
        foreach (Transform childTransform in playerTankView.gameObject.transform)
        {
            // Check if the child transform has the specified tag
            if (childTransform.CompareTag("Player"))
            {
                GameObject.Destroy(childTransform.gameObject);
            }
        }

        StartCoroutine(DestoryAsync());
    }

    IEnumerator DestoryAsync() {
        // Wait 1 second before destorying
        yield return new WaitForSeconds(1f);

        yield return DestroyEnemies();
        yield return DestroyGround();
        yield return DestroyLevelArt();

        foreach (GameObject _gameobject in AllOtherObjects)
        {
            _gameobject.SetActive(false);
        }

        PlayerLostScreen.SetActive(true);
    }

    IEnumerator DestroyEnemies()
    {
        // Wait 1 second before destorying
        yield return new WaitForSeconds(1f);

        EnemyTankService enemyTankService = EnemyTankService.Instance;
        List<EnemyTankController> EnemyTankControllerList = new List<EnemyTankController>();
        if (enemyTankService != null)
            EnemyTankControllerList = enemyTankService.EnemyTankControllerList; 

        // Get Enemy Tanks in the scene
        // Currently enemies werent created under a parent gameObject 
        // So using object based search instead of string based tag search
        // EnemyTankView[] EnemyTanks = GameObject.FindObjectsOfType<EnemyTankView>();
        foreach (EnemyTankController enemyTankController in EnemyTankControllerList)
        {
            try
            {
                EnemyTankView EnemyTank = enemyTankController.EnemyTankView;

                if (EnemyTank.gameObject != null)
                    GameObject.Destroy(EnemyTank.gameObject);
            }
            catch (Exception ex) {
                Debug.LogWarning(ex);
            }
            // Wait 0.5 seconds after destorying
            yield return new WaitForSeconds(.5f);
        }
        
    }

    IEnumerator DestroyGround()
    {
        // Wait 1 second before destorying
        yield return new WaitForSeconds(1f);

        if (GroundGameObject == null)
            yield return null;

        foreach (Transform childTransform in GroundGameObject.transform)
        {
            GameObject.Destroy(childTransform.gameObject);

            // Wait 0.5 seconds after destorying
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator DestroyLevelArt()
    {
        // Wait 1 second before destorying
        yield return new WaitForSeconds(1f);

        if (LevelArtGameObject == null)
            yield return null;

        foreach (Transform childTransform in LevelArtGameObject.transform)
        {
            GameObject.Destroy(childTransform.gameObject);

            // Wait 0.5 seconds after destorying
            yield return new WaitForSeconds(.5f);
        }
    }

    public void RestartLevel()
    {
        SceneBlock scene = gameManager.findScene("Game");
        LoadScene(scene);
    }

    public void MainMenu()
    {
        SceneBlock scene = gameManager.findScene("Initial");
        LoadScene(scene);
    }

    void LoadScene(SceneBlock scene) {
        if (scene != null)
        {
            SceneManager.LoadScene(scene.BuildIndex);
        }
    }

}