using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkips : MonoBehaviour
{
    [SerializeField] public List<Enemy> enemies;
    
    //current level is set to 0 as there is only one level
    //ideally this would be incramented by the game manager on level completion
    public int currentLevel = 0;
    private float levelTime = 0f;
    private float dynamicLevelTime;
    private string currentSkip;
    private List<KeyValuePair<float, string>[]> levelSkips;

    // Track for scratch
    private bool scratched = false;     // starts at false, true once rewinded, false again once skipped
    private float scratchedCountdown;    // countdown between rewind and skip in scratch behavior


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

        // set coutdown for scratch
        scratchedCountdown = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        //bullets = FindObjectsByType<Bullet>(FindObjectsSortMode.None);

        //this code should work as a skip sequencer for levels but I have not tested it(may be useful when testing)
         if (dynamicLevelTime < levelTime)
        {
            dynamicLevelTime += Time.deltaTime;
        }

        if (dynamicLevelTime >= levelTime)
        {
            dynamicLevelTime = levelTime;
        }

        if (dynamicLevelTime >= levelSkips[currentLevel][0].Key)
        {
            currentSkip = levelSkips[currentLevel][0].Value;

            switch (currentSkip)
            {
                case "Rewind":
                    //call rewind method
                    Rewind();
                    break;

                case "Skip":
                    //call skip method
                    SkipForward();
                    break;

                case "Drop":
                    //call drop method
                    NeedleDrop(3.0f);
                    break;

                case "Scratch":
                    //call scratch method
                    switch (scratched)
                    {
                        case false:
                            Rewind();
                            break;
                        case true:
                            if (scratchedCountdown > 0) scratchedCountdown -= Time.deltaTime;
                            else { SkipForward(); scratchedCountdown = 0.3f; }
                            break;
                    }
                    break;
            }
        }
    }

    //add perameters as needed for the methods

    private void SkipForward()
    {
        foreach (Enemy e in enemies) e.SkipForward();
    }

    private void Rewind()
    {
        foreach (Enemy e in enemies) e.Rewind();
    }

    public void NeedleDrop(float yOffset)
    {
        foreach (Enemy e in enemies) e.NeedleDrop(yOffset);
    }

    // handled in switch statement 
    //internal static void RecordScratch(objBullet)
    //{
    //set speed to a fast negative value (like -7) to have bullets move backwards until it is close to spawn position 
    //possibly get check distance from enemy parent 
    //set bullet speed to a positive fast value (move bullets fast forward until all bullets are inactive)
    //}
}
