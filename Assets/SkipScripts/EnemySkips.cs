using UnityEngine;

public static class EnemySkips
{
    //Psuedocode until I can see ellie's code

    internal static void SkipForward(obj bullet)
    {
        //Clean, but only makes sense for linear motion
        //bullet.position.x += XOffset;
        //Might have to look into how into how ellie is programming the enemy log.
    }

    internal static void Rewind(obj bullet)
    {
        //Issues: see SkipForward()
        //bullet.position.x -= XOffset;
    }

    internal static void NeedleDrop(obj bullet)
    {
        //bullet.position.y += YOffset;
    }

    internal static void RecordScratch(objBullet)
    {
        //This is the fun one.
        //bullet.position = spawn.position;
        for (int i = 0; i < 60; i++) //for 60 frames, 1 sec
        {
            //bullet.speed += SpeedOffset;
        }
    }
}
