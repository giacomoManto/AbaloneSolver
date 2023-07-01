using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MarbleColor
{
    white,
    black
}

public class Marble
{
    public Vector2 position;
    public MarbleColor mColor;

    public Marble(MarbleColor color, Vector2 position)
    {
        this.position = position;
        this.mColor = color;
    }
}

public class Column
{
    public Marble[] marbleArray;
    public Vector2 posEdgeOne = Vector2.zero;
    public Vector2 posEdgeTwo = Vector2.zero;
    public MarbleColor mColor;

    private Column()
    {
        if (marbleArray.Length <= 0)
        {
            throw new System.ArgumentNullException("marbles");
        }
        mColor = marbleArray[0].mColor;
        foreach (Marble marble in marbleArray)
        {
            if (mColor != marble.mColor)
            {
                throw new System.ArgumentOutOfRangeException("Column Colors");
            }

            // Checks which marbles of this column are the edge.
            if (posEdgeOne.magnitude > marble.position.magnitude)
            {
                posEdgeOne = marble.position;
            }
            if (posEdgeTwo.magnitude < marble.position.magnitude)
            {
                posEdgeTwo = marble.position;
            }
        }
    }

    public Column(Marble one, Marble two) : this()
    {
        marbleArray = new Marble[2] { one, two };
    }

    public Column(Marble one, Marble two, Marble three) : this()
    {
        marbleArray = new Marble[3] { one, two, three };
    }
}