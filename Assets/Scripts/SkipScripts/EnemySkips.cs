using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkips : MonoBehaviour
{
    public Bullet[] bullets;
    
    //current level is set to 0 as there is only one level
    //ideally this would be incramented by the game manager on level completion
    public int currentLevel = 0;
    private float levelTime = 0f;
    private float dynamicLevelTime;
    private string currentSkip;
    private List<KeyValuePair<float, string>[]> levelSkips;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //level skip index will act as active skip list level
        //use 
        levelSkips.Add(new KeyValuePair<float, string>[]{new KeyValuePair<float,string>(7f,"None"), 
                                                        new KeyValuePair<float,string>(14f,"None"),
                                                        new KeyValuePair<float,string>(22f,"Rewind"),
                                                        new KeyValuePair<float,string>(30f,"Skip"),
                                                        new KeyValuePair<float,string>(38f,"Rewind"),
                                                        new KeyValuePair<float,string>(46f,"Drop"),
                                                        new KeyValuePair<float,string>(54f,"None"),
                                                        new KeyValuePair<float,string>(64f,"Rewind"),
                                                        new KeyValuePair<float,string>(72f,"Scratch"),
                                                        new KeyValuePair<float,string>(75f,"None")});

        foreach(KeyValuePair<float,string> skip in levelSkips[currentLevel])
        {
            levelTime += skip.Key;
        }
        //add 3 seconds from 3 secon buffer (on documentation)
        levelTime += 3;
    }

    // Update is called once per frame
    void Update()
    {
        bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);

        //this code should work as a skip sequencer for levels but I have not tested it (may be useful when testing)
        // if(dynamicLevelTime < levelTime)
        // {
        //     dynamicLevelTime += Time.deltaTime;
        // }

        // if(dynamicLevelTime >= levelTime)
        // {
        //     dynamicLevelTime = levelTime;
        // }

        // if(dynamicLevelTime >= levelSkips[currentLevel][0].Key)
        // {
        //     currentSkip = levelSkips[currentLevel][0].Value;

        //     switch (currentSkip)
        //     {
        //         case "Rewind":
        //             //call rewind method
        //             break;

        //         case "Skip":
        //             //call skip method
        //             break;

        //         case "Drop":
        //             //call drop method
        //             break;

        //         case "Scratch":
        //             //call scratch method
        //             break;
        //     }
        // }
    }

    //add perameters as needed for the methods

    // internal static void SkipForward()
    // {
    //    foreach (Bullet b in bullets)
    //     {
    //         b.transform.position += b.transform.position/2;    
    //     }
    // }

    // internal static void Rewind()
    // {
    //    foreach (Bullet b in bullets)
    //     {
    //         b.transform.position -= b.transform.position/2;    
    //     }
    // }

    // internal static void NeedleDrop(float yOffset)
    // {
    //    foreach (Bullet b in bullets)
    //     {
    //         Vector3 newPos = b.transform.position;
    //         newPos.y -= yOffset;
    //         b.transform.position = newPos;
    //     }
    // }

    //internal static void RecordScratch(objBullet)
    //{
            //set speed to a fast negative value (like -7) to have bullets move backwards until it is close to spawn position 
                //possibly get check distance from enemy parent 
            //set bullet speed to a positive fast value (move bullets fast forward until all bullets are inactive)
    //}
}
