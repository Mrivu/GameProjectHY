using System.Collections.Generic;
using UnityEngine;

public static class TextData
{
    public static Dictionary<int, List<string>> textData = new Dictionary<int, List<string>>() 
    {
        {0, new List<string> { "This text exist solely to see if the text system works and how scrolling text should look like. " +
            "It has no impact on the game nor has it any relevance other than serving as a placeholder for dialogue.", "This Text is the next entry in" +
            " text without any actual impact. It just exists to test if the sytem works." 
        }}
    };
}
