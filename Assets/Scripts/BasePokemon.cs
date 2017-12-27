using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BasePokemon : MonoBehaviour
{
    public string name;
    public Sprite image;
    public int HP;
    private int maxHP;
    public PokemonType type;
    public Rarity rarity;
    public Stat AttackStat;
    public Stat DefenceStat;

    public PokemonStats pokemonStats;

    public bool canEvolve;
    public PokemonEvolution evolveTo;

    private int level;

    void Start()
    {
        maxHP = HP;
    }

    void Update()
    {

    }

    public void AddMember(BasePokemon bp)
    {
        this.name = bp.name;
        this.image = bp.image;
        this.type = bp.type;
        this.rarity = bp.rarity;
        this.HP = bp.HP;
        this.maxHP = bp.maxHP;
        this.AttackStat = bp.AttackStat;
        this.DefenceStat = bp.DefenceStat;
        this.pokemonStats = bp.pokemonStats;
        this.canEvolve = bp.canEvolve;
        this.evolveTo = bp.evolveTo;
        this.level = bp.level;
    }
}

public enum Rarity
{
    VeryCommon,
    Common,
    SemiRare,
    Rare,
    VeryRare
}

public enum PokemonType
{
    Flying,
    Ground,
    Rock,
    Steel,
    Fire,
    Water,
    Grass,
    Ice,
    Electric,
    Psychic,
    Dark,
    Dragon,
    Fighting,
    Normal
}

[System.Serializable]
public class PokemonEvolution
{
    public BasePokemon nextEvolution;
    public int levelUpLevel;
}

[System.Serializable]
public class PokemonStats
{
    public int AttackStat;
    public int DefenceStat;
    public int SpeedStat;
    public int SpAttackStat;
    public int SpDefenceStat;
    public int EvasionStat;
}

[System.Serializable]
public class Stat
{
    public float minimum;
    public float maximum;
}