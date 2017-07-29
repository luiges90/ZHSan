using GameGlobal;
using GameObjects.Animations;
using GameObjects.ArchitectureDetail;
using GameObjects.Conditions;
using GameObjects.FactionDetail;
using GameObjects.Influences;
using GameObjects.MapDetail;
using GameObjects.PersonDetail;
using GameObjects.SectionDetail;
using GameObjects.TroopDetail;
using GameObjects.TroopDetail.EventEffect;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class CommonData
    {

        public static CommonData Current = null;

        public static bool CurrentReady = false;

        [DataMember]
        public ArchitectureKindTable AllArchitectureKinds = new ArchitectureKindTable();
        [DataMember]
        public AttackDefaultKindList AllAttackDefaultKinds = new AttackDefaultKindList();
        [DataMember]
        public AttackTargetKindList AllAttackTargetKinds = new AttackTargetKindList();
        [DataMember]
        public CastDefaultKindList AllCastDefaultKinds = new CastDefaultKindList();
        [DataMember]
        public CastTargetKindList AllCastTargetKinds = new CastTargetKindList();

        [DataMember]
        public List<CharacterKind> AllCharacterKinds = new List<CharacterKind>();
        [DataMember]
        public List<Color> AllColors = new List<Color>();
        [DataMember]
        public CombatMethodTable AllCombatMethods = new CombatMethodTable();

        //性能优化
        [DataMember]
        public ConditionKindTable AllConditionKinds = new ConditionKindTable();
        
        [DataMember]
        public ConditionTable AllConditions = new ConditionTable();

        [DataMember]
        public FacilityKindTable AllFacilityKinds = new FacilityKindTable();
        
        [DataMember]
        public zainanzhongleibiao suoyouzainanzhonglei = new zainanzhongleibiao();

        [DataMember]
        public guanjuezhongleibiao suoyouguanjuezhonglei = new guanjuezhongleibiao();

        [DataMember]
        public GameObjectList AllIdealTendencyKinds = new GameObjectList();

        //性能优化
        [DataMember]
        public InfluenceKindTable AllInfluenceKinds = new InfluenceKindTable();

        [DataMember]
        public InfluenceTable AllInfluences = new InfluenceTable();

        [DataMember]
        public InformationKindList AllInformationKinds = new InformationKindList();

        [DataMember]
        public MilitaryKindTable AllMilitaryKinds = new MilitaryKindTable();

        [DataMember]
        public SectionAIDetailTable AllSectionAIDetails = new SectionAIDetailTable();
        
        [DataMember]
        public SkillTable AllSkills = new SkillTable();
        [DataMember]
        public StratagemTable AllStratagems = new StratagemTable();
        [DataMember]
        public StuntTable AllStunts = new StuntTable();
        [DataMember]
        public TechniqueTable AllTechniques = new TechniqueTable();
        [DataMember]
        public TerrainDetailTable AllTerrainDetails = new TerrainDetailTable();
        [DataMember]
        public TextMessageTable AllTextMessages = new TextMessageTable();
        [DataMember]
        public AnimationTable AllTileAnimations = new AnimationTable();

        [DataMember]
        public TitleTable AllTitles = new TitleTable();
        
        [DataMember]
        public TitleKindTable AllTitleKinds = new TitleKindTable();

        // public GuanzhiTable AllGuanzhis = new GuanzhiTable();
        //public GuanzhiKindTable AllGuanzhiKinds = new GuanzhiKindTable();
        [DataMember]
        public AnimationTable AllTroopAnimations = new AnimationTable();
        
        [DataMember]
        public EventEffectKindTable AllTroopEventEffectKinds = new EventEffectKindTable();

        [DataMember]
        public EventEffectTable AllTroopEventEffects = new EventEffectTable();

        [DataMember]
        public GameObjects.ArchitectureDetail.EventEffect.EventEffectKindTable AllEventEffectKinds = new GameObjects.ArchitectureDetail.EventEffect.EventEffectKindTable();

        [DataMember]
        public GameObjects.ArchitectureDetail.EventEffect.EventEffectTable AllEventEffects = new GameObjects.ArchitectureDetail.EventEffect.EventEffectTable();
        
        [DataMember]
        public List<BiographyAdjectives> AllBiographyAdjectives = new List<BiographyAdjectives>();
        [DataMember]
        public PersonGeneratorSetting PersonGeneratorSetting = new PersonGeneratorSetting();
        [DataMember]
        public PersonGeneratorTypeList AllPersonGeneratorTypes = new PersonGeneratorTypeList();
        [DataMember]
        public TrainPolicyList AllTrainPolicies = new TrainPolicyList();

        public CombatNumberGenerator NumberGenerator = new CombatNumberGenerator();

        public TroopAnimation TroopAnimations = new TroopAnimation();

        public CommonData Clone()
        {
            var commonData = this.MemberwiseClone() as CommonData;
            return commonData;
        }

        public void Clear()
        {
            this.AllArchitectureKinds.Clear();
            this.AllAttackDefaultKinds.Clear();
            this.AllAttackTargetKinds.Clear();
            this.AllBiographyAdjectives.Clear();
            this.AllCastDefaultKinds.Clear();
            this.AllCastTargetKinds.Clear();
            this.AllCharacterKinds.Clear();
            this.AllColors.Clear();
            this.AllCombatMethods.Clear();
            this.AllConditionKinds.Clear();
            this.AllConditions.Clear();
            this.AllEventEffectKinds.Clear();
            this.AllEventEffects.Clear();
            this.AllFacilityKinds.Clear();
            this.AllIdealTendencyKinds.Clear();
            this.AllInfluenceKinds.Clear();
            this.AllInfluences.Clear();
            this.AllInformationKinds.Clear();
            this.AllMilitaryKinds.Clear();
            this.AllSectionAIDetails.Clear();
            this.AllSkills.Clear();
            this.AllStratagems.Clear();
            this.AllStunts.Clear();
            this.AllTechniques.Clear();
            this.AllTerrainDetails.Clear();
            this.AllTextMessages.Clear();
            this.AllTileAnimations.Clear();
            this.AllTitles.Clear();
            this.AllTitleKinds.Clear();
            //this.AllGuanzhis.Clear();
            //this.AllGuanzhiKinds.Clear();
            this.AllTroopAnimations.Clear();
            this.AllTroopEventEffectKinds.Clear();
            this.AllTroopEventEffects.Clear();
            this.suoyouguanjuezhonglei.Clear();
            this.suoyouzainanzhonglei.Clear();
            this.AllPersonGeneratorTypes.Clear();
            this.AllTrainPolicies.Clear();
        }

    }
    
}

