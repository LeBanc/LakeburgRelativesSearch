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
    public bool _isExiled;

    [SerializeField]
    public OriginEnum _villagerOrigin = OriginEnum.Lakeburg;

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

    [SerializeField]
    public string[] _likedTopics = new string[3];
    [SerializeField]
    public string[] _dislikedTopics = new string[3];
    [SerializeField]
    public string _job = "";

    public void CreateVillager(string id, string firstName, string lastName, bool female, int birthYear, string motherId, string fatherId, string partnerId, string[] childrenId, string[] exesId, bool isDead, bool isExiled, int age, string origin, List<string> likedTopics, List<string> dislikedTopics, string job, bool isWorking)
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
        _isExiled = isExiled;
        switch(origin)
        {
            case "PeasantVillager":
            case "NewBornVillager":
                _villagerOrigin = OriginEnum.Lakeburg; break;
            case "NeighbourManToDate":
            case "NeighbourWomanToDate":
                _villagerOrigin = OriginEnum.Tindra; break;
            case "NeighbourManToRecruit":
            case "NeighbourWomanToRecruit":
                _villagerOrigin = OriginEnum.Neighbourhood; break;
            default:
                _villagerOrigin = OriginEnum.Lakeburg; break;
        }

        _likedTopics = likedTopics.ToArray();
        _dislikedTopics = dislikedTopics.ToArray();

        string tempJob = "";
        bool apprentice = false;
        if(job.Contains("Apprentice"))
        {
            apprentice = true;
            job = job.Replace("Apprentice", "");
        }

        switch(job)
        {
            case "Ballroom": // TBC, maybe already Dancer in save file
                tempJob = "Dancer";
                break;
            case "Banquet":
                tempJob = "Taster";
                break;
            case "Beggar":
                tempJob = "Rat trainer";
                break;
            case "Church":
                tempJob = _female ? "Priestess" : "Priest";
                break;
            case "Couturier":
                tempJob = _female ? "Seamstress" : "Seamster";
                break;
            case "Fisherman":
                tempJob = _female ? "Fisherwoman" : "Fisherman";
                break;
            case "Gambling Den": // TBC, maybe already Croupier in save file
                tempJob = "Croupier";
                break;
            case "Harvester":
                tempJob = "Gatherer";
                break;
            case "Hooker":
                tempJob = _female ? "Lady of the evening" : "Man of the evening";
                break;
            case "Hunter":
                tempJob = _female ? "Huntress" : "Hunter";
                break;
            case "Inn":
                tempJob = "Innkeeper";
                break;
            case "Jousting":
                tempJob = "Knight";
                break;
            case "Killer":
                tempJob = "Assassin";
                break;
            case "Manufacturer":
                tempJob = "Carpenter";
                break;
            case "Nomad":
                tempJob = "Prankster";
                break;
            case "Rancher":
                tempJob = "Livestock farmer";
                break;
            case "School":
                tempJob = "Teacher";
                break;
            case "Sovereign":
                tempJob = _female ? "Queen" : "King";
                break;
            case "Stonecutter":
                tempJob = "Mason";
                break;
            case "Theater":
                tempJob = _female ? "Actress" : "Actor";
                break;
            case "Trader":
                tempJob = "Merchant";
                break;
            case "Woodcutter":
                tempJob = "Lumberjack";
                break;
            default:
                tempJob = job;
                break;
        }
        _job = tempJob;
        if (apprentice) _job = "Apprentice " + _job;
        if (!_job.Equals("") && !isWorking) _job = "(" + _job + ")";
    }

    public enum OriginEnum
    {
        Lakeburg,
        Tindra,
        Neighbourhood
    }
}
