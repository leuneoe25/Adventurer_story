using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest
{
    public enum Type
    {
        first,
        Second,
        third
    }
    public string Explanation;
    public string title;
    public GameObject player;
    public string NeedItem;
    public int NeedItemCount;
    public abstract Type Gettype();
    public abstract string Gettitle();
    public abstract string GetExplanation();
    public abstract void Execut();
}

public class WarriorQuest : Quest
{
    Type type = Type.first;

    public override Type Gettype()
    {
        return type;
    }
    public WarriorQuest()
    {
        title = "전대 용사를 물리치세요";
        Explanation = "전대 용사가 타락해버렸습니다. 전대 용사를 물리치세요";
    }

    public override string Gettitle()
    {
        return title;
    }
    public override string GetExplanation()
    {
        return Explanation;
    }
    public override void Execut()
    {
        Debug.Log("a");
    }
}
public class shaderQuest : Quest
{
    Type type = Type.Second;

    public override Type Gettype()
    {
        return type;
    }
    public shaderQuest()
    {
        title = "망령킹을 물리치세요";
        Explanation = "망령킹이 이세상에 나타나버렸습니다.\n망령킹을 물리치세요";
    }

    public override string Gettitle()
    {
        return title;
    }
    public override string GetExplanation()
    {
        return Explanation;
    }
    public override void Execut()
    {
        Debug.Log("b");
    }
}
public class fireQuest : Quest
{
    Type type = Type.third;

    public override Type Gettype()
    {
        return type;
    }
    public fireQuest()
    {
        title = "파이어 데블을 물리치세요";
        Explanation = "파이어 데블이 모든것을 파괴하려합니다.\n파이어 데블을 물리치세요";
    }

    public override string Gettitle()
    {
        return title;
    }
    public override string GetExplanation()
    {
        return Explanation;
    }
    public override void Execut()
    {
        Debug.Log("c");
    }
}

