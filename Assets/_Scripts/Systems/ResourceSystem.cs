using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    public List<ScriptableHero> heroList { get; private set; }
    private Dictionary<HeroType, ScriptableHero> _HeroDictionary;

    protected override void Awake()
    {
        base.Awake();
        AssembleResources();
    }

    private void AssembleResources()
    {
        heroList = Resources.LoadAll<ScriptableHero>("Heroes").ToList();
        _HeroDictionary = heroList.ToDictionary(r => r.heroType, r => r);
    }

    public ScriptableHero GetHero(HeroType t) => _HeroDictionary[t];
    public ScriptableHero GetRandomHero() => heroList[Random.Range(0,heroList.Count)];
}
