using GameObjects;
using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using GameObjects.Influences.InfluenceKindPack;

namespace GameObjects.Influences
{
    [DataContract]
//[KnownType(typeof(InfluenceKind0))]
//[KnownType(typeof(InfluenceKind1))]
//[KnownType(typeof(InfluenceKind2))]
//[KnownType(typeof(InfluenceKind3))]
//[KnownType(typeof(InfluenceKind4))]
//[KnownType(typeof(InfluenceKind5))]
//[KnownType(typeof(InfluenceKind10))]
//[KnownType(typeof(InfluenceKind11))]
//[KnownType(typeof(InfluenceKind12))]
//[KnownType(typeof(InfluenceKind13))]
//[KnownType(typeof(InfluenceKind14))]
//[KnownType(typeof(InfluenceKind15))]
//[KnownType(typeof(InfluenceKind20))]
//[KnownType(typeof(InfluenceKind21))]
//[KnownType(typeof(InfluenceKind22))]
//[KnownType(typeof(InfluenceKind23))]
//[KnownType(typeof(InfluenceKind24))]
//[KnownType(typeof(InfluenceKind25))]
//[KnownType(typeof(InfluenceKind30))]
//[KnownType(typeof(InfluenceKind40))]
//[KnownType(typeof(InfluenceKind41))]
//[KnownType(typeof(InfluenceKind42))]
//[KnownType(typeof(InfluenceKind43))]
//[KnownType(typeof(InfluenceKind50))]
//[KnownType(typeof(InfluenceKind60))]
//[KnownType(typeof(InfluenceKind61))]
//[KnownType(typeof(InfluenceKind62))]
//[KnownType(typeof(InfluenceKind63))]
//[KnownType(typeof(InfluenceKind64))]
//[KnownType(typeof(InfluenceKind65))]
//[KnownType(typeof(InfluenceKind80))]
//[KnownType(typeof(InfluenceKind81))]
//[KnownType(typeof(InfluenceKind90))]
//[KnownType(typeof(InfluenceKind91))]
//[KnownType(typeof(InfluenceKind100))]
//[KnownType(typeof(InfluenceKind120))]
//[KnownType(typeof(InfluenceKind121))]
//[KnownType(typeof(InfluenceKind122))]
//[KnownType(typeof(InfluenceKind123))]
//[KnownType(typeof(InfluenceKind124))]
//[KnownType(typeof(InfluenceKind160))]
//[KnownType(typeof(InfluenceKind200))]
//[KnownType(typeof(InfluenceKind220))]
//[KnownType(typeof(InfluenceKind250))]
//[KnownType(typeof(InfluenceKind251))]
//[KnownType(typeof(InfluenceKind260))]
//[KnownType(typeof(InfluenceKind261))]
//[KnownType(typeof(InfluenceKind262))]
//[KnownType(typeof(InfluenceKind263))]
//[KnownType(typeof(InfluenceKind270))]
//[KnownType(typeof(InfluenceKind271))]
//[KnownType(typeof(InfluenceKind272))]
//[KnownType(typeof(InfluenceKind280))]
//[KnownType(typeof(InfluenceKind281))]
//[KnownType(typeof(InfluenceKind282))]
//[KnownType(typeof(InfluenceKind285))]
//[KnownType(typeof(InfluenceKind290))]
//[KnownType(typeof(InfluenceKind300))]
//[KnownType(typeof(InfluenceKind320))]
//[KnownType(typeof(InfluenceKind330))]
//[KnownType(typeof(InfluenceKind350))]
//[KnownType(typeof(InfluenceKind351))]
//[KnownType(typeof(InfluenceKind352))]
//[KnownType(typeof(InfluenceKind353))]
//[KnownType(typeof(InfluenceKind354))]
//[KnownType(typeof(InfluenceKind360))]
//[KnownType(typeof(InfluenceKind370))]
//[KnownType(typeof(InfluenceKind380))]
//[KnownType(typeof(InfluenceKind381))]
//[KnownType(typeof(InfluenceKind382))]
//[KnownType(typeof(InfluenceKind383))]
//[KnownType(typeof(InfluenceKind384))]
//[KnownType(typeof(InfluenceKind385))]
//[KnownType(typeof(InfluenceKind386))]
//[KnownType(typeof(InfluenceKind387))]
//[KnownType(typeof(InfluenceKind388))]
//[KnownType(typeof(InfluenceKind390))]
//[KnownType(typeof(InfluenceKind391))]
//[KnownType(typeof(InfluenceKind392))]
//[KnownType(typeof(InfluenceKind393))]
//[KnownType(typeof(InfluenceKind394))]
//[KnownType(typeof(InfluenceKind395))]
//[KnownType(typeof(InfluenceKind396))]
//[KnownType(typeof(InfluenceKind397))]
//[KnownType(typeof(InfluenceKind398))]
//[KnownType(typeof(InfluenceKind399))]
//[KnownType(typeof(InfluenceKind400))]
//[KnownType(typeof(InfluenceKind405))]
//[KnownType(typeof(InfluenceKind410))]
//[KnownType(typeof(InfluenceKind415))]
//[KnownType(typeof(InfluenceKind420))]
//[KnownType(typeof(InfluenceKind425))]
//[KnownType(typeof(InfluenceKind430))]
//[KnownType(typeof(InfluenceKind431))]
//[KnownType(typeof(InfluenceKind440))]
//[KnownType(typeof(InfluenceKind450))]
//[KnownType(typeof(InfluenceKind451))]
//[KnownType(typeof(InfluenceKind452))]
//[KnownType(typeof(InfluenceKind453))]
//[KnownType(typeof(InfluenceKind454))]
//[KnownType(typeof(InfluenceKind455))]
//[KnownType(typeof(InfluenceKind456))]
//[KnownType(typeof(InfluenceKind457))]
//[KnownType(typeof(InfluenceKind458))]
//[KnownType(typeof(InfluenceKind460))]
//[KnownType(typeof(InfluenceKind461))]
//[KnownType(typeof(InfluenceKind462))]
//[KnownType(typeof(InfluenceKind463))]
//[KnownType(typeof(InfluenceKind464))]
//[KnownType(typeof(InfluenceKind465))]
//[KnownType(typeof(InfluenceKind466))]
//[KnownType(typeof(InfluenceKind471))]
//[KnownType(typeof(InfluenceKind472))]
//[KnownType(typeof(InfluenceKind473))]
//[KnownType(typeof(InfluenceKind474))]
//[KnownType(typeof(InfluenceKind475))]
//[KnownType(typeof(InfluenceKind500))]
//[KnownType(typeof(InfluenceKind510))]
//[KnownType(typeof(InfluenceKind520))]
//[KnownType(typeof(InfluenceKind530))]
//[KnownType(typeof(InfluenceKind540))]
//[KnownType(typeof(InfluenceKind550))]
//[KnownType(typeof(InfluenceKind560))]
//[KnownType(typeof(InfluenceKind570))]
//[KnownType(typeof(InfluenceKind571))]
//[KnownType(typeof(InfluenceKind572))]
//[KnownType(typeof(InfluenceKind573))]
//[KnownType(typeof(InfluenceKind580))]
//[KnownType(typeof(InfluenceKind581))]
//[KnownType(typeof(InfluenceKind582))]
//[KnownType(typeof(InfluenceKind583))]
//[KnownType(typeof(InfluenceKind584))]
//[KnownType(typeof(InfluenceKind585))]
//[KnownType(typeof(InfluenceKind586))]
//[KnownType(typeof(InfluenceKind590))]
//[KnownType(typeof(InfluenceKind591))]
//[KnownType(typeof(InfluenceKind592))]
//[KnownType(typeof(InfluenceKind593))]
//[KnownType(typeof(InfluenceKind594))]
//[KnownType(typeof(InfluenceKind595))]
//[KnownType(typeof(InfluenceKind596))]
//[KnownType(typeof(InfluenceKind600))]
//[KnownType(typeof(InfluenceKind601))]
//[KnownType(typeof(InfluenceKind602))]
//[KnownType(typeof(InfluenceKind603))]
//[KnownType(typeof(InfluenceKind604))]
//[KnownType(typeof(InfluenceKind605))]
//[KnownType(typeof(InfluenceKind606))]
//[KnownType(typeof(InfluenceKind607))]
//[KnownType(typeof(InfluenceKind608))]
//[KnownType(typeof(InfluenceKind609))]
//[KnownType(typeof(InfluenceKind610))]
//[KnownType(typeof(InfluenceKind611))]
//[KnownType(typeof(InfluenceKind612))]
//[KnownType(typeof(InfluenceKind613))]
//[KnownType(typeof(InfluenceKind615))]
//[KnownType(typeof(InfluenceKind620))]
//[KnownType(typeof(InfluenceKind630))]
//[KnownType(typeof(InfluenceKind640))]
//[KnownType(typeof(InfluenceKind650))]
//[KnownType(typeof(InfluenceKind655))]
//[KnownType(typeof(InfluenceKind660))]
//[KnownType(typeof(InfluenceKind665))]
//[KnownType(typeof(InfluenceKind670))]
//[KnownType(typeof(InfluenceKind675))]
//[KnownType(typeof(InfluenceKind680))]
//[KnownType(typeof(InfluenceKind685))]
//[KnownType(typeof(InfluenceKind690))]
//[KnownType(typeof(InfluenceKind695))]
//[KnownType(typeof(InfluenceKind700))]
//[KnownType(typeof(InfluenceKind710))]
//[KnownType(typeof(InfluenceKind720))]
//[KnownType(typeof(InfluenceKind721))]
//[KnownType(typeof(InfluenceKind800))]
//[KnownType(typeof(InfluenceKind801))]
//[KnownType(typeof(InfluenceKind802))]
//[KnownType(typeof(InfluenceKind803))]
//[KnownType(typeof(InfluenceKind804))]
//[KnownType(typeof(InfluenceKind805))]
//[KnownType(typeof(InfluenceKind824))]
//[KnownType(typeof(InfluenceKind825))]
//[KnownType(typeof(InfluenceKind832))]
//[KnownType(typeof(InfluenceKind833))]
//[KnownType(typeof(InfluenceKind850))]
//[KnownType(typeof(InfluenceKind851))]
//[KnownType(typeof(InfluenceKind852))]
//[KnownType(typeof(InfluenceKind860))]
//[KnownType(typeof(InfluenceKind900))]
//[KnownType(typeof(InfluenceKind910))]
//[KnownType(typeof(InfluenceKind1000))]
//[KnownType(typeof(InfluenceKind1001))]
//[KnownType(typeof(InfluenceKind1002))]
//[KnownType(typeof(InfluenceKind1003))]
//[KnownType(typeof(InfluenceKind1004))]
//[KnownType(typeof(InfluenceKind1005))]
//[KnownType(typeof(InfluenceKind1020))]
//[KnownType(typeof(InfluenceKind1030))]
//[KnownType(typeof(InfluenceKind1040))]
//[KnownType(typeof(InfluenceKind1050))]
//[KnownType(typeof(InfluenceKind1060))]
//[KnownType(typeof(InfluenceKind1070))]
//[KnownType(typeof(InfluenceKind1100))]
//[KnownType(typeof(InfluenceKind1120))]
//[KnownType(typeof(InfluenceKind1200))]
//[KnownType(typeof(InfluenceKind1300))]
//[KnownType(typeof(InfluenceKind2000))]
//[KnownType(typeof(InfluenceKind2010))]
//[KnownType(typeof(InfluenceKind2020))]
//[KnownType(typeof(InfluenceKind2030))]
//[KnownType(typeof(InfluenceKind2100))]
//[KnownType(typeof(InfluenceKind2200))]
//[KnownType(typeof(InfluenceKind2210))]
//[KnownType(typeof(InfluenceKind2220))]
//[KnownType(typeof(InfluenceKind2230))]
//[KnownType(typeof(InfluenceKind2240))]
//[KnownType(typeof(InfluenceKind2250))]
//[KnownType(typeof(InfluenceKind2300))]
//[KnownType(typeof(InfluenceKind2310))]
//[KnownType(typeof(InfluenceKind2320))]
//[KnownType(typeof(InfluenceKind2321))]
//[KnownType(typeof(InfluenceKind2330))]
//[KnownType(typeof(InfluenceKind2331))]
//[KnownType(typeof(InfluenceKind2340))]
//[KnownType(typeof(InfluenceKind2350))]
//[KnownType(typeof(InfluenceKind2360))]
//[KnownType(typeof(InfluenceKind2361))]
//[KnownType(typeof(InfluenceKind2370))]
//[KnownType(typeof(InfluenceKind2371))]
//[KnownType(typeof(InfluenceKind2400))]
//[KnownType(typeof(InfluenceKind2410))]
//[KnownType(typeof(InfluenceKind2420))]
//[KnownType(typeof(InfluenceKind2430))]
//[KnownType(typeof(InfluenceKind2500))]
//[KnownType(typeof(InfluenceKind2510))]
//[KnownType(typeof(InfluenceKind2520))]
//[KnownType(typeof(InfluenceKind2530))]
//[KnownType(typeof(InfluenceKind2540))]
//[KnownType(typeof(InfluenceKind2550))]
//[KnownType(typeof(InfluenceKind2560))]
//[KnownType(typeof(InfluenceKind3000))]
//[KnownType(typeof(InfluenceKind3010))]
//[KnownType(typeof(InfluenceKind3020))]
//[KnownType(typeof(InfluenceKind3030))]
//[KnownType(typeof(InfluenceKind3040))]
//[KnownType(typeof(InfluenceKind3050))]
//[KnownType(typeof(InfluenceKind3060))]
//[KnownType(typeof(InfluenceKind3080))]
//[KnownType(typeof(InfluenceKind3090))]
//[KnownType(typeof(InfluenceKind3091))]
//[KnownType(typeof(InfluenceKind3092))]
//[KnownType(typeof(InfluenceKind3100))]
//[KnownType(typeof(InfluenceKind3110))]
//[KnownType(typeof(InfluenceKind3120))]
//[KnownType(typeof(InfluenceKind3130))]
//[KnownType(typeof(InfluenceKind3140))]
//[KnownType(typeof(InfluenceKind3152))]
//[KnownType(typeof(InfluenceKind3153))]
//[KnownType(typeof(InfluenceKind3154))]
//[KnownType(typeof(InfluenceKind3156))]
//[KnownType(typeof(InfluenceKind3157))]
//[KnownType(typeof(InfluenceKind3200))]
//[KnownType(typeof(InfluenceKind3210))]
//[KnownType(typeof(InfluenceKind3220))]
//[KnownType(typeof(InfluenceKind3230))]
//[KnownType(typeof(InfluenceKind3240))]
//[KnownType(typeof(InfluenceKind3250))]
//[KnownType(typeof(InfluenceKind3260))]
//[KnownType(typeof(InfluenceKind3270))]
//[KnownType(typeof(InfluenceKind3300))]
//[KnownType(typeof(InfluenceKind3310))]
//[KnownType(typeof(InfluenceKind3400))]
//[KnownType(typeof(InfluenceKind4000))]
//[KnownType(typeof(InfluenceKind4010))]
//[KnownType(typeof(InfluenceKind4020))]
//[KnownType(typeof(InfluenceKind4030))]
//[KnownType(typeof(InfluenceKind4040))]
//[KnownType(typeof(InfluenceKind4050))]
//[KnownType(typeof(InfluenceKind4051))]
//[KnownType(typeof(InfluenceKind4060))]
//[KnownType(typeof(InfluenceKind5000))]
//[KnownType(typeof(InfluenceKind5010))]
//[KnownType(typeof(InfluenceKind5020))]
//[KnownType(typeof(InfluenceKind5030))]
//[KnownType(typeof(InfluenceKind5040))]
//[KnownType(typeof(InfluenceKind5050))]
//[KnownType(typeof(InfluenceKind5055))]
//[KnownType(typeof(InfluenceKind5060))]
//[KnownType(typeof(InfluenceKind5070))]
//[KnownType(typeof(InfluenceKind5080))]
//[KnownType(typeof(InfluenceKind5090))]
//[KnownType(typeof(InfluenceKind5100))]
//[KnownType(typeof(InfluenceKind5110))]
//[KnownType(typeof(InfluenceKind6000))]
//[KnownType(typeof(InfluenceKind6010))]
//[KnownType(typeof(InfluenceKind6020))]
//[KnownType(typeof(InfluenceKind6030))]
//[KnownType(typeof(InfluenceKind6040))]
//[KnownType(typeof(InfluenceKind6050))]
//[KnownType(typeof(InfluenceKind6060))]
//[KnownType(typeof(InfluenceKind6070))]
//[KnownType(typeof(InfluenceKind6100))]
//[KnownType(typeof(InfluenceKind6110))]
//[KnownType(typeof(InfluenceKind6120))]
//[KnownType(typeof(InfluenceKind6130))]
//[KnownType(typeof(InfluenceKind6140))]
//[KnownType(typeof(InfluenceKind6150))]
//[KnownType(typeof(InfluenceKind6155))]
//[KnownType(typeof(InfluenceKind6160))]
//[KnownType(typeof(InfluenceKind6165))]
//[KnownType(typeof(InfluenceKind6170))]
//[KnownType(typeof(InfluenceKind6175))]
//[KnownType(typeof(InfluenceKind6180))]
//[KnownType(typeof(InfluenceKind6190))]
//[KnownType(typeof(InfluenceKind6210))]
//[KnownType(typeof(InfluenceKind6220))]
//[KnownType(typeof(InfluenceKind6230))]
//[KnownType(typeof(InfluenceKind6240))]
//[KnownType(typeof(InfluenceKind6250))]
//[KnownType(typeof(InfluenceKind6260))]
//[KnownType(typeof(InfluenceKind6265))]
//[KnownType(typeof(InfluenceKind6270))]
//[KnownType(typeof(InfluenceKind6280))]
//[KnownType(typeof(InfluenceKind6285))]
//[KnownType(typeof(InfluenceKind6300))]
//[KnownType(typeof(InfluenceKind6310))]
//[KnownType(typeof(InfluenceKind6320))]
//[KnownType(typeof(InfluenceKind6330))]
//[KnownType(typeof(InfluenceKind6340))]
//[KnownType(typeof(InfluenceKind6350))]
//[KnownType(typeof(InfluenceKind6360))]
//[KnownType(typeof(InfluenceKind6370))]
//[KnownType(typeof(InfluenceKind6400))]
//[KnownType(typeof(InfluenceKind6410))]
//[KnownType(typeof(InfluenceKind6420))]
//[KnownType(typeof(InfluenceKind6430))]
//[KnownType(typeof(InfluenceKind6440))]
//[KnownType(typeof(InfluenceKind6450))]
//[KnownType(typeof(InfluenceKind6460))]
//[KnownType(typeof(InfluenceKind6470))]
//[KnownType(typeof(InfluenceKind6480))]
//[KnownType(typeof(InfluenceKind6490))]
//[KnownType(typeof(InfluenceKind6495))]
//[KnownType(typeof(InfluenceKind6500))]
//[KnownType(typeof(InfluenceKind6505))]
//[KnownType(typeof(InfluenceKind6510))]
//[KnownType(typeof(InfluenceKind6515))]
//[KnownType(typeof(InfluenceKind6520))]
//[KnownType(typeof(InfluenceKind6525))]
//[KnownType(typeof(InfluenceKind6530))]
//[KnownType(typeof(InfluenceKind6535))]
//[KnownType(typeof(InfluenceKind6540))]
//[KnownType(typeof(InfluenceKind6545))]
//[KnownType(typeof(InfluenceKind6550))]
//[KnownType(typeof(InfluenceKind6555))]
//[KnownType(typeof(InfluenceKind6560))]
//[KnownType(typeof(InfluenceKind6565))]
//[KnownType(typeof(InfluenceKind6570))]
//[KnownType(typeof(InfluenceKind6575))]
//[KnownType(typeof(InfluenceKind6580))]
//[KnownType(typeof(InfluenceKind6585))]
//[KnownType(typeof(InfluenceKind6595))]
//[KnownType(typeof(InfluenceKind6600))]
//[KnownType(typeof(InfluenceKind6610))]
//[KnownType(typeof(InfluenceKind6620))]
//[KnownType(typeof(InfluenceKind6630))]
//[KnownType(typeof(InfluenceKind6700))]
//[KnownType(typeof(InfluenceKind6705))]
//[KnownType(typeof(InfluenceKind6710))]
//[KnownType(typeof(InfluenceKind6715))]
//[KnownType(typeof(InfluenceKind6720))]
//[KnownType(typeof(InfluenceKind6725))]
//[KnownType(typeof(InfluenceKind6730))]
//[KnownType(typeof(InfluenceKind6735))]
//[KnownType(typeof(InfluenceKind6740))]
//[KnownType(typeof(InfluenceKind6745))]
//[KnownType(typeof(InfluenceKind6750))]
//[KnownType(typeof(InfluenceKind6755))]
//[KnownType(typeof(InfluenceKind6760))]
//[KnownType(typeof(InfluenceKind6770))]
//[KnownType(typeof(InfluenceKind6775))]
//[KnownType(typeof(InfluenceKind6780))]
//[KnownType(typeof(InfluenceKind6785))]
//[KnownType(typeof(InfluenceKind6790))]
//[KnownType(typeof(InfluenceKind6795))]
//[KnownType(typeof(InfluenceKind6800))]
//[KnownType(typeof(InfluenceKind6810))]
//[KnownType(typeof(InfluenceKind6820))]
//[KnownType(typeof(InfluenceKind6830))]
//[KnownType(typeof(InfluenceKind6835))]
//[KnownType(typeof(InfluenceKind6840))]
//[KnownType(typeof(InfluenceKind6845))]
//[KnownType(typeof(InfluenceKind6850))]
//[KnownType(typeof(InfluenceKind6855))]
//[KnownType(typeof(InfluenceKind6900))]
//[KnownType(typeof(InfluenceKind6905))]
//[KnownType(typeof(InfluenceKind6910))]
//[KnownType(typeof(InfluenceKind6915))]
//[KnownType(typeof(InfluenceKind6920))]
//[KnownType(typeof(InfluenceKind6925))]
//[KnownType(typeof(InfluenceKind6930))]
//[KnownType(typeof(InfluenceKind6935))]
//[KnownType(typeof(InfluenceKind6945))]
//[KnownType(typeof(InfluenceKind6990))]
//[KnownType(typeof(InfluenceKind6995))]
    public class InfluenceKind : GameObject  //abstract
    {
        [DataMember]
        public bool TroopLeaderValid;
        private InfluenceType type;
        private bool combat;

        [DataMember]
        public InfluenceType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        [DataMember]
        public bool Combat
        {
            get
            {
                return combat;
            }
            set
            {
                combat = value;
            }
        }

        [DataMember]
        public float AIPersonValue { get; set; }

        [DataMember]
        public float AIPersonValuePow { get; set; }

#pragma warning disable CS0169 // The field 'InfluenceKind.p1' is never used
#pragma warning disable CS0169 // The field 'InfluenceKind.p2' is never used
        private float p1, p2;
#pragma warning restore CS0169 // The field 'InfluenceKind.p2' is never used
#pragma warning restore CS0169 // The field 'InfluenceKind.p1' is never used

        public virtual void ApplyInfluenceKind(Architecture a)
        {
        }

        public void ApplyInfluenceKind(Architecture architecture, Influence i, Applier applier, int applierID)
        {
            if (this.Type == InfluenceType.建筑 || this.Type == InfluenceType.建筑战斗)
            {
                if (i.appliedArch.Add(new ApplyingArchitecture(architecture, applier, applierID)))
                {
                    ApplyInfluenceKind(architecture);
                }
            } 
            else if (this.Type == InfluenceType.个人)
            {
                foreach (Person p in architecture.Persons)
                {
                    ApplyInfluenceKind(p, i, applier, applierID, false);
                }
            }
        }

        public virtual void ApplyInfluenceKind(Faction f)
        {
        }

        public void ApplyInfluenceKind(Faction faction, Influence i, Applier applier, int applierID)
        {
            if (this.Type == InfluenceType.势力)
            {
                if (i.appliedFaction.Add(new ApplyingFaction(faction, applier, applierID)))
                {
                    ApplyInfluenceKind(faction);
                }
            }
            if (this.Type == InfluenceType.建筑 || this.Type == InfluenceType.建筑战斗)
            {
                foreach (Architecture a in faction.Architectures)
                {
                    ApplyInfluenceKind(a, i, applier, applierID);
                }
            }
            if (this.Type == InfluenceType.战斗 || this.Type == InfluenceType.建筑战斗)
            {
                foreach (Troop t in faction.Troops)
                {
                    ApplyInfluenceKind(t, i, applier, applierID);
                }
            }
            if (this.Type == InfluenceType.个人)
            {
                foreach (Person p in faction.Persons)
                {
                    ApplyInfluenceKind(p, i, applier, applierID, false);
                }
            }
        }

        public virtual void ApplyInfluenceKind(Person p)
        {
        }

        public void ApplyInfluenceKind(Person person, Influence i, Applier applier, int applierID, bool excludePersonal)
        {
            if (this.Type == InfluenceType.个人)
            {
                if (i.appliedPerson.Add(new ApplyingPerson(person, applier, applierID)))
                {
                    ApplyInfluenceKind(person);
                }
            }
            if (this.Type == InfluenceType.战斗 || this.Type == InfluenceType.建筑战斗)
            {
                if (person.LocationTroop != null)
                {
                    ApplyInfluenceKind(person.LocationTroop, i, applier, applierID);
                }
            }
            if (this.Type == InfluenceType.建筑 || this.Type == InfluenceType.建筑战斗)
            {
                if (person.LocationArchitecture != null)
                {
                    ApplyInfluenceKind(person.LocationArchitecture, i, applier, applierID);
                }
            }
        }

        public virtual void ApplyInfluenceKind(Troop t)
        {
        }

        public void ApplyInfluenceKind(Troop troop, Influence i, Applier applier, int applierID)
        {
            if (this.Type == InfluenceType.战斗 || this.Type == InfluenceType.建筑战斗)
            {
                if (i.appliedTroop.Add(new ApplyingTroop(troop, applier, applierID)) || (this.ID >= 390 && this.ID <= 399) || this.ID == 720 || this.ID == 721)
                {
                    troop.InfluencesApplying.Add(i);
                    ApplyInfluenceKind(troop);
                }
            }
        }

        public virtual void DoWork(Architecture architecture)
        {
        }

        public virtual int GetCredit(Troop source, Troop destination)
        {
            return 0;
        }

        public virtual int GetCreditWithPosition(Troop source, out Point? position)
        {
            //position = 0;
            position = new Point(0, 0);
            return 0;
        }

        public virtual void InitializeParameter(string parameter)
        {
        }

        public virtual void InitializeParameter2(string parameter)
        {
        }

        public virtual bool IsVaild(Person person)
        {
            return true;
        }

        public virtual bool IsVaild(Troop troop)
        {
            return true;
        }

        public virtual void PurifyInfluenceKind(Architecture a)
        {
        }

        public void PurifyInfluenceKind(Architecture architecture, Influence i, Applier applier, int applierID)
        {
            if (this.Type == InfluenceType.建筑 || this.Type == InfluenceType.建筑战斗)
            {
                if (i.appliedArch.Remove(new ApplyingArchitecture(architecture, applier, applierID)))
                {
                    PurifyInfluenceKind(architecture);
                }
            }
            else if (this.Type == InfluenceType.个人)
            {
                foreach (Person p in architecture.Persons)
                {
                    PurifyInfluenceKind(p, i, applier, applierID, false);
                }
            }
        }

        public virtual void PurifyInfluenceKind(Faction f)
        {
        }

        public void PurifyInfluenceKind(Faction faction, Influence i, Applier applier, int applierID)
        {
            if (this.Type == InfluenceType.势力)
            {
                if (i.appliedFaction.Remove(new ApplyingFaction(faction, applier, applierID)))
                {
                    PurifyInfluenceKind(faction);
                }
                foreach (Architecture a in faction.Architectures)
                {
                    PurifyInfluenceKind(a, i, applier, applierID);
                }
                foreach (Troop t in faction.Troops)
                {
                    PurifyInfluenceKind(t, i, applier, applierID);
                }
            }
        }

        public virtual void PurifyInfluenceKind(Person p)
        {
        }

        public void PurifyInfluenceKind(Person person, Influence i, Applier applier, int applierID, bool excludePersonal)
        {
            if (this.Type == InfluenceType.个人)
            {
                if (i.appliedPerson.Remove(new ApplyingPerson(person, applier, applierID)))
                {
                    PurifyInfluenceKind(person);
                }
            }
            if (this.Type == InfluenceType.战斗 || this.Type == InfluenceType.建筑战斗)
            {
                if (person.LocationTroop != null)
                {
                    PurifyInfluenceKind(person.LocationTroop, i, applier, applierID);
                }
            }
            if (this.Type == InfluenceType.建筑 || this.Type == InfluenceType.建筑战斗)
            {
                if (person.LocationArchitecture != null)
                {
                    PurifyInfluenceKind(person.LocationArchitecture, i, applier, applierID);
                }
            }
        }

        public virtual void PurifyInfluenceKind(Troop t)
        {
        }

        public void PurifyInfluenceKind(Troop troop, Influence i, Applier applier, int applierID)
        {
            if (this.Type == InfluenceType.战斗 || this.Type == InfluenceType.建筑战斗)
            {
                if (i.appliedTroop.Remove(new ApplyingTroop(troop, applier, applierID)))
                {
                    PurifyInfluenceKind(troop);
                }
            }
        }

        public virtual double AIFacilityValue(Architecture a)
        {
            return 0;
        }

    }
}

