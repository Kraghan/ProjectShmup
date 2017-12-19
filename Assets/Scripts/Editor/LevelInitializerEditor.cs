using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UI;

#if UNITY_EDITOR
[CustomEditor(typeof(LevelInitializer), true)]
public class LevelInitializerEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Instantiate all objects"))
        {
            if (((LevelInitializer)target).camera != null)
            {
                bool hasEnemyPool = false;
                bool hasBulletPool = false;
                bool hasSpawnerTrigger = false;

                for (int i = 0; i < ((LevelInitializer)target).camera.transform.GetChildCount(); ++i)
                {
                    GameObject child = ((LevelInitializer)target).camera.transform.GetChild(i).gameObject;
                    if (child.CompareTag("EnemyRepository"))
                        hasEnemyPool = true;
                    if (child.CompareTag("BulletRepository"))
                        hasBulletPool = true;
                    if (child.GetComponent<Collider2D>() != null && child.GetComponent<Rigidbody2D>() != null)
                        hasSpawnerTrigger = true;
                }

                if (!hasEnemyPool)
                    Debug.LogWarning("Your camera prefab doesn't contain any object with tag 'EnemyRepository' : It may cause some troubles");
                if (!hasBulletPool)
                    Debug.LogWarning("Your camera prefab doesn't contain any object with tag 'BulletRepository' : It may cause some troubles");
                if (!hasSpawnerTrigger)
                    Debug.LogWarning("Your camera prefab doesn't contain any object with a Collider2D and/or a RigidBody2D : It won't trigger enemy spawner");

                GameObject camera = Instantiate(((LevelInitializer)target).camera.gameObject);

                if(((LevelInitializer)target).UI != null)
                {
                    GameObject obj = Instantiate(((LevelInitializer)target).UI.gameObject);
                    obj.transform.parent = camera.transform;
                }
                else
                    Debug.LogWarning("You don't set the 'UI' prefab");

                if (((LevelInitializer)target).playerSpawner != null)
                {
                    GameObject obj = Instantiate(((LevelInitializer)target).playerSpawner.gameObject);
                    obj.transform.parent = camera.transform;
                }
                else
                    Debug.LogWarning("You don't set the 'PlayerSpawner' prefab");

            }
            else
                Debug.LogWarning("You don't set the 'camera' prefab");

            GameObject managers = new GameObject("Managers");
            
            if(((LevelInitializer)target).gameManager != null)
            {
                if (((LevelInitializer)target).gameManager.gameObject.GetComponent<ComboCalculator>() == null)
                    Debug.LogWarning("Your gameManager prefab doesn't contain a component 'ComboCalculator' : The combo won't work");

                GameObject obj = Instantiate(((LevelInitializer)target).gameManager.gameObject);
                obj.transform.parent = managers.transform;
            }
            else
                Debug.LogWarning("You don't set the 'GameManager' prefab");

            if (((LevelInitializer)target).musicManager != null)
            {
                // TODO 

                GameObject obj = Instantiate(((LevelInitializer)target).musicManager.gameObject);
                obj.transform.parent = managers.transform;
            }
            else
                Debug.LogWarning("You don't set the 'MusicManager' prefab");

        }
    }

}

#endif
