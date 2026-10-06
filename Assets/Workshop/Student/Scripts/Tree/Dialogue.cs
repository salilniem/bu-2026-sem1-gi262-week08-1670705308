using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public string text;
    public Dictionary<string, Dialogue> nexts = new Dictionary<string, Dialogue>();

    public Dialogue(string text)
    {
        this.text = text;
        this.nexts = new Dictionary<string, Dialogue>();
    }

    public void AddNextNode(string choiceText, Dialogue nextNode)
    {
        if (!nexts.ContainsKey(choiceText))
        {
            nexts.Add(choiceText, nextNode);
        }
    }

    public void AddNext(Dialogue nextNode, string choiceText)
    {
        AddNextNode(choiceText, nextNode);
    }

    public void Print()
    {
        Debug.Log("NPC: " + text);
        var choiceText = new List<string>(nexts.Keys);
        for (int i = 0; i < choiceText.Count; i++)
        {
            Debug.Log("    +-- [" + (i + 1) + "] " + choiceText[i]);
        }
        Debug.Log("--------------------");
    }
}

public class DialogueTree
{
    public Dialogue rootNode;

    public DialogueTree(Dialogue rootNode)
    {
        this.rootNode = rootNode;
    }
}