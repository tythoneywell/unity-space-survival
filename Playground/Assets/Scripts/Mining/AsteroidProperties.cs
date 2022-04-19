using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is a static class that simply holds health and amount values for
 * each type of asteroid (so we can just edit one place for all the asteroids)
 */
public static class AsteroidProperties
{
    public static int CopperAsteroidHealth = 100;
    public static int MiniCopperAsteroidHealth = 25;
    public static int MaxCopperMineableAmount = 10;

    public static int IceAsteroidHealth = 100;
    public static int MiniIceAsteroidHealth = 25;
}
