using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public enum type
    {
        Use,
        Weapon,
        Portion,
        AdditionHp,
        AdditionPower,
        MonsterItem
    }
    public string name;
    public string Exp;
    public int price;
    public Sprite image;
    public abstract type Gettype();
    public abstract int Get();
}
public class ItemDictionary
{
    private Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();
    
    public void AddItem(string name,Item item)
    {
        itemDictionary.Add(name, item);
    }
    public Item GetItem(string name)
    {
        return itemDictionary[name];
    }
}
//----------Weapon
class RustedDagger : Item
{
    private int damage = 2;

    public RustedDagger(string _name,int _price,Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;

    }
    public type type = type.Weapon;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return damage;
    }
}
class Doubleedgedsword: Item
{
    private int damage = 5;

    public Doubleedgedsword(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;

    }
    public type type = type.Weapon;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return damage;
    }
}
class Uglyonehandedsword: Item
{
    private int damage = 7;

    public Uglyonehandedsword(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;

    }
    public type type = type.Weapon;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return damage;
    }
}
class Plaindoubleedgedsword: Item
{
    private int damage = 10;

    public Plaindoubleedgedsword(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;

    }
    public type type = type.Weapon;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return damage;
    }
}
class sharpkatana: Item
{
    private int damage = 17;

    public sharpkatana(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;

    }
    public type type = type.Weapon;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return damage;
    }
}
class keeldiagram: Item
{
    private int damage = 20;

    public keeldiagram(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;

    }
    public type type = type.Weapon;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return damage;
    }
}
class HolySwordEscanor : Item
{
    private int damage = 35;

    public HolySwordEscanor(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;

    }
    public type type = type.Weapon;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return damage;
    }
}
//----------Use
class EmergencyPotion : Item
{
    private int healamount = 10;

    public EmergencyPotion(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;
        Exp = "-응급포션\n체력 10 % 회복";
    }
    public type type = type.Portion;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return healamount;
    }
}
class HealingPotion: Item
{
    private int healamount = 20;

    public HealingPotion(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;
        Exp = "-힐링포션\n체력 20 % 회복";
    }
    public type type = type.Portion;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return healamount;
    }
}
class Highhealingpotion : Item
{
    private int healamount = 40;

    public Highhealingpotion(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;
        Exp = "-하이힐링포션\n체력 40 % 회복";
    }
    public type type = type.Portion;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return healamount;
    }
}
class Elixir : Item
{
    private int healamount = 100;

    public Elixir(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;
    }
    public type type = type.Portion;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return healamount;
    }
}
//--비약
class HpAddition : Item
{
    private int Add = 50;
    public HpAddition(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;
    }
    public type type = type.AdditionHp;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return Add;
    }
}
class DamageAddition : Item
{
    private int Add = 5;
    public DamageAddition(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;
    }
    public type type = type.AdditionPower;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return Add;
    }
}
//몬스터 아이템
class Boen : Item
{
    private int Add = 0;
    public Boen(string _name, int _price, Sprite _sprite)
    {
        name = _name;
        price = _price;
        image = _sprite;
    }
    public type type = type.MonsterItem;
    public override type Gettype()
    {
        return type;
    }
    public override int Get()
    {
        return Add;
    }
}
