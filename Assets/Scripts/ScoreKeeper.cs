using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreKeeper{
    public static int activeHole = 1;
    public static int hole1Strokes = 0;
    static bool hole1Complete = false;
    public static int hole2Strokes = 0;
    static bool hole2Complete = false;
    public static int hole3Strokes = 0;
    static bool hole3Complete = false;
    public static int hole4Strokes = 0;
    static bool hole4Complete = false;
    static int totalScore = 0;

    public static void sunkBall()
    {
        switch (activeHole)
        {
            case 1:
                hole1Complete = true;
                break;
            case 2:
                hole2Complete = true;
                break;
            case 3:
                hole3Complete = true;
                break;
            case 4:
                hole4Complete = true;
                break;
        }
    }

    public static void invalidBallSink()
    {
        switch (activeHole)
        {
            case 1:
                hole1Complete = false;
                break;
            case 2:
                hole2Complete = false;
                break;
            case 3:
                hole3Complete = false;
                break;
            case 4:
                hole4Complete = false;
                break;
        }
    }

    public static bool ballInHole()
    {
        switch (activeHole)
        {
            case 1:
                return hole1Complete;
            case 2:
                return hole2Complete;
            case 3:
                return hole3Complete;
            case 4:
                return hole4Complete;
            default:
                return false; //No active hole
        }
    }

    public static int getStrokes()
    {
        switch (activeHole)
        {
            case 1:
                return hole1Strokes;
            case 2:
                return hole2Strokes;
            case 3:
                return hole3Strokes;
            case 4:
                return hole4Strokes;
            default:
                return -1; //No active hole
        }
    }

    public static void addStroke()
    {
        switch (activeHole)
        {
            case 1:
                if (!hole1Complete)
                {
                    hole1Strokes++;
                }
                break;
            case 2:
                if (!hole2Complete)
                {
                    hole2Strokes++;
                }
                break;
            case 3:
                if (!hole3Complete)
                {
                    hole3Strokes++;
                }
                break;
            case 4:
                if (!hole4Complete)
                {
                    hole4Strokes++;
                }
                break;
        }
    }

    public static void resetActiveHole()
    {
        switch (activeHole)
        {
            case 1:
                hole1Strokes = 0;
                break;
            case 2:
                hole2Strokes = 0;
                break;
            case 3:
                hole3Strokes = 0;
                break;
            case 4:
                hole4Strokes = 0;
                break;
        }

        computeScore();
    }
    
    public static int computeScore()
    {
        totalScore = hole1Strokes + hole2Strokes + hole3Strokes + hole4Strokes;

        return totalScore;
    }

    public static void restartGame()
    {
        activeHole = 1;
        hole1Strokes = 0;
        hole1Complete = false;
        hole2Strokes = 0;
        hole2Complete = false;
        hole3Strokes = 0;
        hole3Complete = false;
        hole4Strokes = 0;
        hole4Complete = false;
        totalScore = 0;
    }
}
