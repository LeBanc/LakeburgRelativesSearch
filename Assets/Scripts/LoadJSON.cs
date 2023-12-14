using System;
using UnityEngine;
using SimpleFileBrowser;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class LoadJSON : MonoBehaviour
{
    #region Savefile Definition
    [Serializable]
    public class SaveFile
    {
        public DemoManagerSerialized DemoManagerSerialized;
        public TimeManagerSerialized TimeManagerSerialized;
        public TownManagerSerialized TownManagerSerialized;
        public NeighbourhoodManagerSerialized NeighbourhoodManagerSerialized;
        public TutorialManagerSerialized TutorialManagerSerialized;
        public TrackedDataManagerSerialized TrackedDataManagerSerialized;
        public RunAchievementsOwnerSerialized RunAchievementsOwnerSerialized;
        public ApplicationVersionSerialized ApplicationVersionSerialized;
        public int AutoSaveCount;
        public string VersionString;
    }

    [Serializable]
    public class DemoManagerSerialized
    {
        public string DemoEndDate;
        public bool DemoEndDateInitialized;

        public DemoManagerSerialized(string demoEndDate, bool demoEndDateInitialized)
        {
            this.DemoEndDate = "";
            this.DemoEndDateInitialized = demoEndDateInitialized;
        }
    }

    [Serializable]
    public class TimeManagerSerialized
    {
        public string CurrentDate;
        public string EndDate;

        public TimeManagerSerialized(string currentDate, string endDate)
        {
            this.CurrentDate = currentDate;
            this.EndDate = endDate;
        }
    }

    [Serializable]
    public class TownManagerSerialized
    {
        public BuildingOwnerSerialized BuildingOwnerSerialized;
        public DatingOwnerSerialized DatingOwnerSerialized;
        public SocialRelationOwnerSerialized SocialRelationOwnerSerialized;
        public EventOwnerSerialized EventOwnerSerialized;
        public ResourcesOwnerSerialized ResourcesOwnerSerialized;
        public RoadSerialized RoadSerialized;
        public ServicesOwnerSerialized ServicesOwnerSerialized;
        public TownJournalOwnerSerialized TownJournalOwnerSerialized;
        public VillagerOwnerSerialized VillagerOwnerSerialized;
        public YearReportOwnerSerialized YearReportOwnerSerialized;
        public VillagerNamePoolSerialized VillagerNamePoolSerialized;
        public DecreeOwnerSerialized DecreeOwnerSerialized;
    }

    [Serializable]
    public class BuildingOwnerSerialized
    {
        public BuildingSerialized[] BuildingsSerialized;
        public BuildingsTimelinesSerialized[] BuildingsTimelines;
        public bool IsAccessibleToPlayerSerialized;
    }
    [Serializable]
    public class BuildingSerialized
    {
        public BuildingEffectsSerialized BuildingEffects;
        public bool IsBuilt;
        public BuildingSlotSerialized[] BuildingSlotsSerialized;
        public BuildingUpgradesSerialized BuildingUpgrades;
        public StudentSlotsGroupSerialized[] StudentSlotsGroupSerialized;
    }
    [Serializable]
    public class BuildingEffectsSerialized
    {
        public BuildingProdEffectsSerialized BuildingProdEffects;
    }
    [Serializable]
    public class BuildingProdEffectsSerialized
    {
        public ProdEffectSerialized[] ProdEffectSerialized;
    }
    [Serializable]
    public class ProdEffectSerialized
    {
        public string type;

        public ProdEffectSerialized()
        {
            this.type = null;
        }
    }
    [Serializable]
    public class BuildingSlotSerialized
    {
        public string type;
        public int MentoringGauge;
        public int Gauge;
        public bool IsLocked;
        public string VillagerId;
    }
    [Serializable]
    public class BuildingUpgradesSerialized
    {
        public UpgradeSerialized[] UpgradeSerialized;
    }
    [Serializable]
    public class UpgradeSerialized
    {
        public string Id;
        public int Level;
    }
    [Serializable]
    public class StudentSlotsGroupSerialized
    {
        public StudentSlotSerialized[] StudentSlotsSerialized;
    }
    [Serializable]
    public class StudentSlotSerialized
    {
        public string type;
        public int AspirationEvolChancesGaugeTime;
        public double AspirationEvolChancesRaw;
        public double AspirationEvolChancesBonus;
        public int MentoringGauge;
        public int Gauge;
        public bool IsLocked;
        public string VillagerId;
    }
    [Serializable]
    public class BuildingsTimelinesSerialized
    {
        public int CurrentBuildingLevel;
    }

    [Serializable]
    public class DatingOwnerSerialized
    {
        public DatingSpaceSerialized DatingSpaceSerialized;
        public bool IsAccessibleToPlayerSerialized;
        public MeetingSpaceSerialized MeetingSpaceSerialized;
    }
    [Serializable]
    public class DatingSpaceSerialized
    {
        public DatingSlot LeftSlot;
        public DatingSlot RightSlot;
        public string Couple;
        public DatingLocationPool DatingLocationPool;
    }
    [Serializable]
    public class DatingSlot
    {
        public bool IsInTown;
        public string VillagerId;
    }
    [Serializable]
    public class DatingLocationPool
    {
        public string[] DatingLocationRemainingId;
    }
    [Serializable]
    public class MeetingSpaceSerialized
    {
        public string[] RemainingPickupLines;
    }

    [Serializable]
    public class SocialRelationOwnerSerialized
    {
        public int DayCount;
    }

    [Serializable]
    public class EventOwnerSerialized
    {
        public EventDrawer DeckDrawer;
        public EventDrawer RoyalDrawer;
    }
    [Serializable]
    public class EventDrawer
    {
        public double Chances;
        public int DaysCooldown;
        public int DaysDraw;
        public int DaysReset;
        public string[] Deck;
    }

    [Serializable]
    public class ResourcesOwnerSerialized
    {
        public ResourceSerialized[] Resources;
    }
    [Serializable]
    public class ResourceSerialized
    {
        public int Value;
        public int MaxStock;
    }

    [Serializable]
    public class RoadSerialized
    {
        public string type;
        public House[] Houses;
        public bool IsAccessibleToPlayerSerialized;
    }
    [Serializable]
    public class House
    {
        public HouseSlot[] ChildSlots;
        public HouseSlot Owner;
    }
    [Serializable]
    public class HouseSlot
    {
        public bool IsLocked;
        public string VillagerId;
    }

    [Serializable]
    public class ServicesOwnerSerialized
    {
        public ServiceSerialized[] ServicesSerialized;
    }
    [Serializable]
    public class ServiceSerialized
    {
        public bool Activated;
        public double CurrentValue;
        public ServiceStep[] ServiceStepsSerialized;
        public ServiceSource[] ServiceSourceSerialized;
        public ServiceModifierSerialized ServiceModifierSerialized;

    }
    [Serializable]
    public class ServiceStep
    {
        public ServiceEffect[] ServiceEffectsSerialized;
    }
    [Serializable]
    public class ServiceEffect
    {
        public string type;
        public int RemainingDays;
    }
    [Serializable]
    public class ServiceSource
    {
        public string SourceId;
        public SubSource[] SubSources;
    }
    [Serializable]
    public class SubSource
    {
        public string SourceId;
        public double Amount;
    }
    [Serializable]
    public class ServiceModifierSerialized
    {
        public double ValueToAdd;
        public int Duration;
    }

    [Serializable]
    public class TownJournalOwnerSerialized
    {
        public AllFiltersSerialized AllFiltersSerialized;
    }
    [Serializable]
    public class AllFiltersSerialized
    {
        public FiltersDrawer[] allFiltersSerialized;
    }
    [Serializable]
    public class FiltersDrawer
    {
        public Filter[] FiltersSerialized;
    }
    [Serializable]
    public class Filter
    {
        public string Id;
        public bool FilterEnabled;
    }

    [Serializable]
    public class VillagerOwnerSerialized
    {
        public string VillagerOwnerJson;
        public VillagerSerialized[] VillagersSerialized;
    }
    [Serializable]
    public class VillagerSerialized
    {
        public ClassesOwnerSerialized ClassesOwnerSerialized;
        public CitizenSerialized CitizenSerialized;
        public DatingProfileSerialized DatingProfileSerialized;
        public FamilySerialized FamilySerialized;
        public IdentitySerialized IdentitySerialized;
        public bool IsDead;
        public bool IsExiled;
        public string LastWorkId;
        public LegacySerialized LegacySerialized;
        public ResourcesOwnerSerialized ResourcesOwnerSerialized;
        public StatOwnerSerialized StatOwnerSerialized;
        public TopicsOwnerSerialized TopicsOwnerSerialized;
        public TraitsOwnerSerialized TraitsOwnerSerialized;
        public SocialRelationsSerialized SocialRelationsSerialized;
        public string VillagerScriptableId;
    }
    [Serializable]
    public class ClassesOwnerSerialized
    {
        public string CurrentClassName;
        public ClassHistory[] HistoryClassesSerialized;
        public int ClassTransitionTime;
        public int ClassProductionTime;
    }
    [Serializable]
    public class ClassHistory
    {
        public string ClassName;
        public string Date;
    }
    [Serializable]
    public class CitizenSerialized
    {
        public double AccumulatedExperience;
        public NeedOwnerSerialized NeedOwnerSerialized;
    }
    [Serializable]
    public class NeedOwnerSerialized
    {
        public NeedResource[] QuantifiedNeedsSerialized;
    }
    [Serializable]
    public class NeedResource
    {
        public string NeedResourceId;
    }
    [Serializable]
    public class DatingProfileSerialized
    {
        public string[] History;
        public string HiddenStatsSerialized;
        public int NopePaidCount;
        public string PickupLine;
        public int ResetNopeTimer;
        public string[] SeenProfiles;
    }
    [Serializable]
    public class FamilySerialized
    {
        public string[] ChildsId;
        public MarriedCoupleSerialized MarriedCoupleSerialized;
    }
    [Serializable]
    public class MarriedCoupleSerialized
    {
        public HistorySerialized HistorySerialized;
        public int BabyTimer;
        public bool NoBabies;
        public CoupleAffectionSerialized CoupleAffectionSerialized;
        public string OwnerId;
        public string PartnerId;
    }
    [Serializable]
    public class HistorySerialized
    {
        public HistoryEvent[] History;
        public int RemainingTime;
    }
    [Serializable]
    public class HistoryEvent
    {
        public string key;
        public string[] parameters;
    }
    [Serializable]
    public class CoupleAffectionSerialized
    {
        public int DaysRemaining;
        public double Value;
    }
    [Serializable]
    public class IdentitySerialized
    {
        public string Id;
        public PhysicalIdentitySerialize PhysicalIdentitySerialize;
        public int SexualOrientationIndex;
        public SerializedNames SerializedNames;
    }
    [Serializable]
    public class PhysicalIdentitySerialize
    {
        public bool Female;
        public PortraitIdentity PortraitIdentity;
        public PhysicalAging PhysicalAging;
    }
    [Serializable]
    public class PortraitIdentity
    {
        public PortraitStep[] PortraitStepsSerialized;
    }
    [Serializable]
    public class PortraitStep
    {
        public GraphicalElement[] BodyParts;
        public GraphicalElement[] ColorPalettes;
        public int UnemployedClothIndex;
    }
    [Serializable]
    public class GraphicalElement
    {
        public string Key;
        public int PortraitInfoIndex;
    }
    [Serializable]
    public class PhysicalAging
    {
        public int Age;
        public AgeStep[] AgeStepsSerialized;
        public int BirthDay;
        public int BirthMonth;
        public int BirthYear;
        public bool IsDead;
    }
    [Serializable]
    public class AgeStep
    {
        public int Age;
    }
    [Serializable]
    public class SerializedNames
    {
        public string RomanFirstName;
        public string RomanLastName;
    }
    [Serializable]
    public class LegacySerialized
    {
        public string[] ExPartnersId;
        public HeritableTitlesOwnerSerialized HeritableTitlesOwnerSerialized;
        public GeneticSerialized GeneticSerialized;
    }
    [Serializable]
    public class HeritableTitlesOwnerSerialized
    {
        public string[] TitlesHistorySerialized;
    }
    [Serializable]
    public class GeneticSerialized
    {
        public string[] ChildsId;
        public string FatherId;
        public bool HasBeenAdopted;
        public string MotherId;
    }
    [Serializable]
    public class StatOwnerSerialized
    {
        public StatSerialized[] StatsSerialize;
    }
    [Serializable]
    public class StatSerialized
    {
        public string Type;
        public int DaysCount;
        public double Value;
    }
    [Serializable]
    public class TopicsOwnerSerialized
    {
        public TopicSerialized[] TopicsSerialized;
    }
    [Serializable]
    public class TopicSerialized
    {
        public string Id;
        public bool IsLiked;
    }
    [Serializable]
    public class TraitsOwnerSerialized
    {
        public TraitSerialized[] TraitsSerialized;
    }
    [Serializable]
    public class TraitSerialized
    {
        public bool HasBeenLearned;
        public int RemainingDays;
        public TraitEffect[] TraitEffectsSerialized;
        public string TraitId;
        public int Level;
        public bool IsDormant;
        public int AspirationEvolCycle;
    }
    [Serializable]
    public class TraitEffect
    {
        public int RemainingDays;
    }
    [Serializable]
    public class SocialRelationsSerialized
    {
        public string VillagerId;
        public RelationSerialized[] Relations;

    }
    [Serializable]
    public class RelationSerialized
    {
        public string VillagerId;
        public string OtherVillagerId;
        public int Category;
        public int Level;
    }

    [Serializable]
    public class YearReportOwnerSerialized
    {
        public YearData[] AllYearsdata;
    }
    [Serializable]
    public class YearData
    {
        public Part[] PartsData;
        public bool ReportComplete;
        public string Id;
    }
    [Serializable]
    public class Part
    {
        public string Type;
        public int AdultsNb;
        public Counts[] BirthsNb;
        public ClassAmount[] ClassesAmounts;
        public Counts[] DeathsNb;
        public int FemalesNb;
        public int MaleNb;
        public Counts[] VillagersNb;
        public Counts[] WeddingsNb;
        public string Id;
    }
    [Serializable]
    public class Counts
    {
        public int CurrentYearData;
        public int AllYearsData;
    }
    [Serializable]
    public class ClassAmount
    {
        public string Text;
        public double Amount;
    }

    [Serializable]
    public class VillagerNamePoolSerialized
    {
        public string PoolFemaleFirstNames;
        public string PoolMaleFirstNames;
        public string PoolLastNames;
    }

    [Serializable]
    public class DecreeOwnerSerialized
    {
        public DecreeSerialized ResourceDecreeSerialized;
        public DecreeSerialized CombatDecreeSerialized;
        public int MonthCountSerialized;
        public double VitalNeedRatioSerialized;
    }
    [Serializable]
    public class DecreeSerialized
    {
        public Decree[] ClassOrdered;
    }
    [Serializable]
    public class Decree
    {
        public int ClassOrdered;
        public bool CanBeMoved;
    }

    [Serializable]
    public class NeighbourhoodManagerSerialized
    {
        public NeighbourOwnerSerialized NeighbourOwnerSerialized;
        public NeighbourRecruitmentSerialized NeighbourRecruitmentSerialized;
        public NeighbourTradingSerialized NeighbourTradingSerialized;
    }
    [Serializable]
    public class NeighbourOwnerSerialized
    {
        public DatingPoolSerialized DatingPoolSerialized;
        public RecruitmentPoolSerialized RecruitmentPoolSerialized;
    }
    [Serializable]
    public class DatingPoolSerialized
    {
        public NeighbourSerialized[] NeighboursSerialized;
    }
    [Serializable]
    public class NeighbourSerialized
    {
        public string Type;
        public bool MustBeReset;
        public string VillagerPatternId;
        public CountdownRange CountdownRange;
        public int RemainingTimeBeforeReset;
        public bool ResetTimeActive;
        public VillagerSerialized VillagerSerialized;
    }
    [Serializable]
    public class CountdownRange
    {
        public int x;
        public int y;
        public double magnitude;
        public int sqrMagnitude;
    }
    [Serializable]
    public class RecruitmentPoolSerialized
    {
        public NeighbourSerialized[] NeighboursSerialized;
    }
    [Serializable]
    public class NeighbourRecruitmentSerialized
    {
        public bool IsAccessibleToPlayerSerialized;
        public bool IsRecruitmentAvailable;
        public int NewProposalTimer;
        public int RecruitmentCooldown;
        public int UsesCount;
        public NeighbourSlot FemaleSlotSerialized;
        public NeighbourSlot MaleSlotSerialized;
    }
    [Serializable]
    public class NeighbourSlot
    {
        public bool MustBeReset;
        public int RerollCount;
        public string VillagerId;
    }
    [Serializable]
    public class NeighbourTradingSerialized
    {
        public bool IsTradingAvailable;
        public int NewProposalTimer;
        public int TradingCooldown;
        public int NbTradingDeal;
        public int NbTradingCallUsed;
        public TradingSpaceInfo[] TradingSpaceInfo;
    }
    [Serializable]
    public class TradingSpaceInfo
    {
        public int Status;
        public string AskedResource;
        public int AskedAmount;
        public string GainedResource;
        public int GainedAmount;
    }

    [Serializable]
    public class TutorialManagerSerialized
    {
        public TutorialSequencesOwnerSerialized TutorialSequencesOwnerSerialized;
    }
    [Serializable]
    public class TutorialSequencesOwnerSerialized
    {
        public Sequence[] Sequences;
    }
    [Serializable]
    public class Sequence
    {
        public string Id;
        public int State;
        public Entry[] EntriesSerialized;
        public SequenceDataListSerialized SequenceDataListSerialized;
    }
    [Serializable]
    public class Entry
    {
        public string Id;
        public int State;
    }
    [Serializable]
    public class SequenceDataListSerialized
    {
        public string[] DynamicDataSerialized;
    }

    [Serializable]
    public class TrackedDataManagerSerialized
    {
        public DataTrackersOwnerSerialized DataTrackersOwnerSerialized;
        public ObjectivesOwnerSerialized ObjectivesOwnerSerialized;
    }
    [Serializable]
    public class DataTrackersOwnerSerialized
    {
        public StackedDataTracker[] StackedDataTrackerSerialized;
    }
    [Serializable]
    public class StackedDataTracker
    {
        public string Id;
        public double CurrentValue;
    }
    [Serializable]
    public class ObjectivesOwnerSerialized
    {
        public Objectives[] ObjectivesSerialized;
    }
    [Serializable]
    public class Objectives
    {
        public string Id;
        public bool IsCompleted;
        public bool HasBeenDisplayed;
        public ObjectiveConditionsSerialized ObjectiveConditionsSerialized;
    }
    [Serializable]
    public class ObjectiveConditionsSerialized
    {
        public Condition[] ConditionsSerialized;
    }
    [Serializable]
    public class Condition
    {
        public bool IsConditionMet;
    }

    [Serializable]
    public class RunAchievementsOwnerSerialized
    {
        public Achievements[] AchievementsSerialized;
    }
    [Serializable]
    public class Achievements
    {
        public string Id;
        public bool IsCompleted;
        public ObjectiveConditionsSerialized ObjectiveConditionsSerialized;
    }
    [Serializable]
    public class ApplicationVersionSerialized
    {
        public int ReleaseVersion;
        public int MajorVersion;
        public int MinorVersion;
        public int FixVersion;
    }
    #endregion

    private VillagersManager villagersManager;
    private RelativesManager relativesManager;
    private MainPageManager mainPageManager;
    private GraveyardManager graveyardManager;
    private MagicalBookManager magicalBookManager;


    void Awake()
    {
        villagersManager = FindFirstObjectByType<VillagersManager>();
        relativesManager = FindFirstObjectByType<RelativesManager>();
        mainPageManager = FindFirstObjectByType<MainPageManager>();
        graveyardManager = FindFirstObjectByType<GraveyardManager>();
        magicalBookManager = FindFirstObjectByType<MagicalBookManager>();
    }

    void Start()
    {
        // Init file browser
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Text Files", ".txt"));
        FileBrowser.SetDefaultFilter(".txt");
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
        FileBrowser.AddQuickLink("Desktop", "C:\\Users\\<user>\\Desktop\\", null);
        FileBrowser.AddQuickLink("Lakeburg Legacy Saves", "C:\\Users\\<user>\\AppData\\LocalLow\\Ishtar Games\\Lakeburg Legacies\\Save\\", null);

        // Load file at start (because we cannot do much without a save file)
        StartCoroutine(LoadFile());
    }

    private IEnumerator LoadFile()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, "C:\\Users\\<user>\\AppData\\LocalLow\\Ishtar Games\\Lakeburg Legacies\\Save\\", null, "Load Save File", "Load");

        if (FileBrowser.Success)
        {
            StreamReader sr = new StreamReader(FileBrowser.Result[0]);
            string fileContents = sr.ReadToEnd();
            sr.Close();
            InitJson(fileContents);
        }
    }

    private void InitJson(string saveFileText)
    {
        // Clear current villager list
        villagersManager.ClearAll();

        // Get data from selected save file
        SaveFile save = new SaveFile();
        string JSONString = saveFileText;
        JSONString = JSONString.Substring(JSONString.IndexOf("{"));
        JSONString = JSONString.Remove(JSONString.Length - 1);
        JsonUtility.FromJsonOverwrite(JSONString, save);

        // Get villagers and students that are currently in a building slot
        List<string> villagerAtWork = new List<string>();
        foreach(BuildingSerialized building in save.TownManagerSerialized.BuildingOwnerSerialized.BuildingsSerialized)
        {
            if (building.BuildingSlotsSerialized != null)
            {
                foreach (BuildingSlotSerialized slot in building.BuildingSlotsSerialized)
                {
                    if (slot.VillagerId != null && !slot.VillagerId.Equals("")) villagerAtWork.Add(slot.VillagerId);
                }
            }
            if(building.StudentSlotsGroupSerialized != null)
            {
                foreach (StudentSlotsGroupSerialized slotGroup in building.StudentSlotsGroupSerialized)
                {
                    if(slotGroup.StudentSlotsSerialized != null)
                    {
                        foreach (StudentSlotSerialized slot in slotGroup.StudentSlotsSerialized)
                        {
                            if (slot.VillagerId != null && !slot.VillagerId.Equals("")) villagerAtWork.Add(slot.VillagerId);
                        }
                    }
                }
            }            
        }

        // Get data for all villagers
        foreach (VillagerSerialized villager in save.TownManagerSerialized.VillagerOwnerSerialized.VillagersSerialized)
        {
            // Get the liked and disliked topics
            List<string> likedTopics = new List<string>();
            List<string> dislikedTopics = new List<string>();
            foreach (TopicSerialized topic in villager.TopicsOwnerSerialized.TopicsSerialized)
            {
                if(topic.IsLiked == true)
                {
                    likedTopics.Add(topic.Id);
                }
                else
                {
                    dislikedTopics.Add(topic.Id);
                }
            }

            // Check if the villager is currently working
            bool isWorking = villagerAtWork.Contains(villager.IdentitySerialized.Id);

            // Get the dat for portrait
            PortraitStep _base = villager.IdentitySerialized.PhysicalIdentitySerialize.PortraitIdentity.PortraitStepsSerialized[0];
            PortraitStep _child = villager.IdentitySerialized.PhysicalIdentitySerialize.PortraitIdentity.PortraitStepsSerialized[2];
            PortraitStep _old = villager.IdentitySerialized.PhysicalIdentitySerialize.PortraitIdentity.PortraitStepsSerialized[4];

            int _hair, _hairBehind, _eyeBrows, _beard, _eyes, _mouth, _nose, _wrinkles = _nose = _mouth = _eyes = _beard = _eyeBrows = _hairBehind = _hair = 0;
            int _childHair, _childHairBehind, _childMouth, _childFace = _childMouth = _childHairBehind = _childHair = 0;
            int _skinColor, _hairColor, _eyeColor, _oldHairColor = _eyeColor = _hairColor = _skinColor = 0;

            int _age;
            if(villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.Age > villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.AgeStepsSerialized[4].Age)
            {
                _age = 4;
            }
            else if (villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.Age > villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.AgeStepsSerialized[3].Age)
            {
                _age = 3;
            }
            else if (villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.Age > villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.AgeStepsSerialized[2].Age)
            {
                _age = 2;
            }
            else if (villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.Age > villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.AgeStepsSerialized[1].Age)
            {
                _age = 1;
            }
            else { _age = 0;}

            foreach (GraphicalElement el in _base.BodyParts)
            {
                if (el == null) continue;
                else if (el.Key == null) continue;
                else if (el.Key.Equals("Hair")) _hair = el.PortraitInfoIndex;
                else if (el.Key.Equals("HairBehind")) _hairBehind = el.PortraitInfoIndex;
                else if (el.Key.Equals("Eye")) _eyes = el.PortraitInfoIndex;
                else if (el.Key.Equals("EyeBrow")) _eyeBrows = el.PortraitInfoIndex;
                else if (el.Key.Equals("Beard")) _beard = el.PortraitInfoIndex;
                else if (el.Key.Equals("Mouse")) _mouth = el.PortraitInfoIndex;
                else if (el.Key.Equals("Nose")) _nose = el.PortraitInfoIndex;
            }

            foreach (GraphicalElement el in _base.ColorPalettes)
            {
                if (el == null) continue;
                else if (el.Key == null) continue;
                else if (el.Key.Equals("Hair")) _hairColor = el.PortraitInfoIndex;
                else if (el.Key.Equals("Eye")) _eyeColor = el.PortraitInfoIndex;
                else if (el.Key.Equals("Skin")) _skinColor = el.PortraitInfoIndex;
            }

            foreach (GraphicalElement el in _child.BodyParts)
            {
                if (el == null) continue;
                else if (el.Key == null) continue;
                else if (el.Key.Equals("Hair")) _childHair = el.PortraitInfoIndex;
                else if (el.Key.Equals("HairBehind")) _childHairBehind = el.PortraitInfoIndex;
                else if (el.Key.Equals("Mouse")) _childMouth = el.PortraitInfoIndex;
                else if (el.Key.Equals("Face")) _childFace = el.PortraitInfoIndex;
            }

            foreach (GraphicalElement el in _old.BodyParts)
            {
                if (el == null) continue;
                else if (el.Key == null) continue;
                else if (el.Key.Equals("Wrinkle")) _wrinkles = el.PortraitInfoIndex;
            }

            foreach (GraphicalElement el in _old.ColorPalettes)
            {
                if (el == null) continue;
                else if (el.Key == null) continue;
                else if (el.Key.Equals("Hair")) _oldHairColor = el.PortraitInfoIndex;
            }

            int[] _portrait;
            switch (_age)
            {
                case 0:
                    _portrait = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, _skinColor, _hairColor, _eyeColor};
                    break;
                case 1:
                    _portrait = new int[] {1, _childFace, _childHair, _childHairBehind, 0, 0, 0, _childMouth, 0, 0, _skinColor, _hairColor, _eyeColor };
                    break;
                case 2:
                    _portrait = new int[] {2, 0, _hair, _hairBehind, _eyes, _eyeBrows, 0, _mouth, _nose, 0, _skinColor, _hairColor, _eyeColor };
                    break;
                case 4:
                    _portrait = new int[] {4, 0, _hair, _hairBehind, _eyes, _eyeBrows, _beard, _mouth, _nose, _wrinkles, _skinColor, _oldHairColor, _eyeColor };
                    break;
                default:
                    _portrait = new int[] {3, 0, _hair, _hairBehind, _eyes, _eyeBrows, _beard, _mouth, _nose, 0, _skinColor, _hairColor, _eyeColor };
                    break;

            }


            // Add villager to the villagers list with all his/her data
            VillagerData v = ScriptableObject.CreateInstance<VillagerData>();
            v.CreateVillager(villager.IdentitySerialized.Id,
                              villager.IdentitySerialized.SerializedNames.RomanFirstName,
                              villager.IdentitySerialized.SerializedNames.RomanLastName,
                              villager.IdentitySerialized.PhysicalIdentitySerialize.Female,
                              villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.BirthYear,
                              villager.LegacySerialized.GeneticSerialized.MotherId,
                              villager.LegacySerialized.GeneticSerialized.FatherId,
                              villager.FamilySerialized.MarriedCoupleSerialized.PartnerId,
                              villager.LegacySerialized.GeneticSerialized.ChildsId,
                              villager.LegacySerialized.ExPartnersId,
                              villager.IsDead,
                              villager.IsExiled,
                              villager.IdentitySerialized.PhysicalIdentitySerialize.PhysicalAging.Age,
                              villager.VillagerScriptableId,
                              likedTopics,
                              dislikedTopics,
                              villager.LastWorkId,
                              isWorking,
                              _portrait);
            villagersManager.villagers.Add(v);
        }

        // When all villagers are added, update legacies and init the villagers scrollbar
        villagersManager.UpdateAllVillagers();
        villagersManager.SortVillagersByOldest();
        villagersManager.SearchAllConnections();
        villagersManager.CreateVillagerObjects();
        villagersManager.SetScrollBarLeft();

        // Set the year in several panels
        relativesManager.year.text = save.TimeManagerSerialized.CurrentDate.Substring(0, 4);
        graveyardManager.year.text = save.TimeManagerSerialized.CurrentDate.Substring(0, 4);
        magicalBookManager.year.text = save.TimeManagerSerialized.CurrentDate.Substring(0, 4);

        // Init the main page and the graveyard
        mainPageManager.ClearDropConteners();        
        graveyardManager.UpdateGraveyard();
    }

    public void LoadAndInitJson()
    {
        StartCoroutine(LoadFile());
    }
}
