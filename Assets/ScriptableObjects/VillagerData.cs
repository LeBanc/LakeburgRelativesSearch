using System.Collections.Generic;
using UnityEngine;

public class VillagerData : ScriptableObject
{
    [SerializeField]
    public string _id;
    [SerializeField]
    public string _firstName;
    [SerializeField]
    public string _lastName;
    [SerializeField]
    public bool _female;
    [SerializeField]
    public int _birthYear;
    [SerializeField]
    public string _motherId;
    [SerializeField]
    public string _fatherId;
    [SerializeField]
    public string _partnerId;
    [SerializeField]
    public string[] _childrenId;
    [SerializeField]
    public string[] _exesId;
    [SerializeField]
    public bool _isDead;
    [SerializeField]
    public int _deathYear;

    [SerializeField]
    public VillagerData _mother;
    [SerializeField]
    public VillagerData _father;
    [SerializeField]
    public VillagerData _partner;
    [SerializeField]
    public List<VillagerData> _children = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _exes = new List<VillagerData>();

    [SerializeField]
    public List<VillagerData> _greatGrandParents = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _grandParents = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _stepGrandParents = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _grandParentsInLaw = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _grandPiblings = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _parents = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _stepParents = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _parentsInLaw = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _piblings = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _siblings = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _halfSiblings = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _stepSiblings = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _siblingsInLaw = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _cousins = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _partnerExes = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _coParentsInLaw = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _stepChildren = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _childrenInLaw = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _niblings = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _grandChildren = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _stepGrandChildren = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _grandChildrenInLaw = new List<VillagerData>();
    [SerializeField]
    public List<VillagerData> _grandNiblings = new List<VillagerData>();    
    [SerializeField]
    public List<VillagerData> _greatGrandChildren = new List<VillagerData>();
    
    

    public void CreateVillager(string id, string firstName, string lastName, bool female, int birthYear, string motherId, string fatherId, string partnerId, string[] childrenId, string[] exesId, bool isDead, int age)
    {
        name = id;
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _female = female;
        _birthYear = birthYear;
        _motherId = motherId;
        _fatherId = fatherId;
        _partnerId = partnerId;
        _childrenId = childrenId;
        _exesId = exesId;
        _isDead = isDead;
        _deathYear = birthYear + age;
    }
}
