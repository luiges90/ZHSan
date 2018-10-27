using GameObjects;
using GameObjects.Conditions.ConditionKindPack;
using System;
using System.Runtime.Serialization;

namespace GameObjects.Conditions
{
    [DataContract]
    //[KnownType(typeof(ConditionKind0))]
    //[KnownType(typeof(ConditionKind100))]
    //[KnownType(typeof(ConditionKind110))]
    //[KnownType(typeof(ConditionKind120))]
    //[KnownType(typeof(ConditionKind130))]
    //[KnownType(typeof(ConditionKind140))]
    //[KnownType(typeof(ConditionKind150))]
    //[KnownType(typeof(ConditionKind160))]
    //[KnownType(typeof(ConditionKind170))]
    //[KnownType(typeof(ConditionKind180))]
    //[KnownType(typeof(ConditionKind190))]
    //[KnownType(typeof(ConditionKind200))]
    //[KnownType(typeof(ConditionKind210))]
    //[KnownType(typeof(ConditionKind220))]
    //[KnownType(typeof(ConditionKind230))]
    //[KnownType(typeof(ConditionKind240))]
    //[KnownType(typeof(ConditionKind300))]
    //[KnownType(typeof(ConditionKind350))]
    //[KnownType(typeof(ConditionKind360))]
    //[KnownType(typeof(ConditionKind370))]
    //[KnownType(typeof(ConditionKind380))]
    //[KnownType(typeof(ConditionKind390))]
    //[KnownType(typeof(ConditionKind400))]
    //[KnownType(typeof(ConditionKind410))]
    //[KnownType(typeof(ConditionKind420))]
    //[KnownType(typeof(ConditionKind425))]
    //[KnownType(typeof(ConditionKind430))]
    //[KnownType(typeof(ConditionKind435))]
    //[KnownType(typeof(ConditionKind440))]
    //[KnownType(typeof(ConditionKind450))]
    //[KnownType(typeof(ConditionKind500))]
    //[KnownType(typeof(ConditionKind505))]
    //[KnownType(typeof(ConditionKind510))]
    //[KnownType(typeof(ConditionKind515))]
    //[KnownType(typeof(ConditionKind520))]
    //[KnownType(typeof(ConditionKind525))]
    //[KnownType(typeof(ConditionKind530))]
    //[KnownType(typeof(ConditionKind535))]
    //[KnownType(typeof(ConditionKind540))]
    //[KnownType(typeof(ConditionKind545))]
    //[KnownType(typeof(ConditionKind550))]
    //[KnownType(typeof(ConditionKind570))]
    //[KnownType(typeof(ConditionKind580))]
    //[KnownType(typeof(ConditionKind590))]
    //[KnownType(typeof(ConditionKind591))]
    //[KnownType(typeof(ConditionKind592))]
    //[KnownType(typeof(ConditionKind593))]
    //[KnownType(typeof(ConditionKind594))]
    //[KnownType(typeof(ConditionKind595))]
    //[KnownType(typeof(ConditionKind600))]
    //[KnownType(typeof(ConditionKind610))]
    //[KnownType(typeof(ConditionKind620))]
    //[KnownType(typeof(ConditionKind621))]
    //[KnownType(typeof(ConditionKind625))]
    //[KnownType(typeof(ConditionKind626))]
    //[KnownType(typeof(ConditionKind630))]
    //[KnownType(typeof(ConditionKind631))]
    //[KnownType(typeof(ConditionKind635))]
    //[KnownType(typeof(ConditionKind636))]
    //[KnownType(typeof(ConditionKind640))]
    //[KnownType(typeof(ConditionKind641))]
    //[KnownType(typeof(ConditionKind645))]
    //[KnownType(typeof(ConditionKind646))]
    //[KnownType(typeof(ConditionKind650))]
    //[KnownType(typeof(ConditionKind655))]
    //[KnownType(typeof(ConditionKind660))]
    //[KnownType(typeof(ConditionKind670))]
    //[KnownType(typeof(ConditionKind680))]
    //[KnownType(typeof(ConditionKind700))]
    //[KnownType(typeof(ConditionKind750))]
    //[KnownType(typeof(ConditionKind760))]
    //[KnownType(typeof(ConditionKind800))]
    //[KnownType(typeof(ConditionKind801))]
    //[KnownType(typeof(ConditionKind802))]
    //[KnownType(typeof(ConditionKind803))]
    //[KnownType(typeof(ConditionKind804))]
    //[KnownType(typeof(ConditionKind805))]
    //[KnownType(typeof(ConditionKind806))]
    //[KnownType(typeof(ConditionKind807))]
    //[KnownType(typeof(ConditionKind808))]
    //[KnownType(typeof(ConditionKind809))]
    //[KnownType(typeof(ConditionKind810))]
    //[KnownType(typeof(ConditionKind818))]
    //[KnownType(typeof(ConditionKind820))]
    //[KnownType(typeof(ConditionKind830))]
    //[KnownType(typeof(ConditionKind840))]
    //[KnownType(typeof(ConditionKind850))]
    //[KnownType(typeof(ConditionKind855))]
    //[KnownType(typeof(ConditionKind860))]
    //[KnownType(typeof(ConditionKind865))]
    //[KnownType(typeof(ConditionKind870))]
    //[KnownType(typeof(ConditionKind875))]
    //[KnownType(typeof(ConditionKind880))]
    //[KnownType(typeof(ConditionKind885))]
    //[KnownType(typeof(ConditionKind890))]
    //[KnownType(typeof(ConditionKind895))]
    //[KnownType(typeof(ConditionKind900))]
    //[KnownType(typeof(ConditionKind901))]
    //[KnownType(typeof(ConditionKind902))]
    //[KnownType(typeof(ConditionKind910))]
    //[KnownType(typeof(ConditionKind911))]
    //[KnownType(typeof(ConditionKind912))]
    //[KnownType(typeof(ConditionKind913))]
    //[KnownType(typeof(ConditionKind914))]
    //[KnownType(typeof(ConditionKind915))]
    //[KnownType(typeof(ConditionKind916))]
    //[KnownType(typeof(ConditionKind917))]
    //[KnownType(typeof(ConditionKind918))]
    //[KnownType(typeof(ConditionKind919))]
    //[KnownType(typeof(ConditionKind920))]
    //[KnownType(typeof(ConditionKind921))]
    //[KnownType(typeof(ConditionKind922))]
    //[KnownType(typeof(ConditionKind923))]
    //[KnownType(typeof(ConditionKind924))]
    //[KnownType(typeof(ConditionKind925))]
    //[KnownType(typeof(ConditionKind930))]
    //[KnownType(typeof(ConditionKind940))]
    //[KnownType(typeof(ConditionKind950))]
    //[KnownType(typeof(ConditionKind951))]
    //[KnownType(typeof(ConditionKind952))]
    //[KnownType(typeof(ConditionKind953))]
    //[KnownType(typeof(ConditionKind954))]
    //[KnownType(typeof(ConditionKind955))]
    //[KnownType(typeof(ConditionKind956))]
    //[KnownType(typeof(ConditionKind957))]
    //[KnownType(typeof(ConditionKind958))]
    //[KnownType(typeof(ConditionKind959))]
    //[KnownType(typeof(ConditionKind965))]
    //[KnownType(typeof(ConditionKind970))]
    //[KnownType(typeof(ConditionKind971))]
    //[KnownType(typeof(ConditionKind975))]
    //[KnownType(typeof(ConditionKind998))]
    //[KnownType(typeof(ConditionKind999))]
    //[KnownType(typeof(ConditionKind1000))]
    //[KnownType(typeof(ConditionKind1001))]
    //[KnownType(typeof(ConditionKind1002))]
    //[KnownType(typeof(ConditionKind1003))]
    //[KnownType(typeof(ConditionKind1004))]
    //[KnownType(typeof(ConditionKind1005))]
    //[KnownType(typeof(ConditionKind1006))]
    //[KnownType(typeof(ConditionKind1007))]
    //[KnownType(typeof(ConditionKind1008))]
    //[KnownType(typeof(ConditionKind1009))]
    //[KnownType(typeof(ConditionKind1010))]
    //[KnownType(typeof(ConditionKind1015))]
    //[KnownType(typeof(ConditionKind1020))]
    //[KnownType(typeof(ConditionKind1025))]
    //[KnownType(typeof(ConditionKind1030))]
    //[KnownType(typeof(ConditionKind1035))]
    //[KnownType(typeof(ConditionKind1040))]
    //[KnownType(typeof(ConditionKind1045))]
    //[KnownType(typeof(ConditionKind1050))]
    //[KnownType(typeof(ConditionKind1051))]
    //[KnownType(typeof(ConditionKind1060))]
    //[KnownType(typeof(ConditionKind1065))]
    //[KnownType(typeof(ConditionKind1100))]
    //[KnownType(typeof(ConditionKind1105))]
    //[KnownType(typeof(ConditionKind1110))]
    //[KnownType(typeof(ConditionKind1115))]
    //[KnownType(typeof(ConditionKind1120))]
    //[KnownType(typeof(ConditionKind1125))]
    //[KnownType(typeof(ConditionKind1130))]
    //[KnownType(typeof(ConditionKind1135))]
    //[KnownType(typeof(ConditionKind1140))]
    //[KnownType(typeof(ConditionKind1145))]
    //[KnownType(typeof(ConditionKind1150))]
    //[KnownType(typeof(ConditionKind1155))]
    //[KnownType(typeof(ConditionKind1160))]
    //[KnownType(typeof(ConditionKind1165))]
    //[KnownType(typeof(ConditionKind1170))]
    //[KnownType(typeof(ConditionKind1175))]
    //[KnownType(typeof(ConditionKind1180))]
    //[KnownType(typeof(ConditionKind1185))]
    //[KnownType(typeof(ConditionKind1190))]
    //[KnownType(typeof(ConditionKind1195))]
    //[KnownType(typeof(ConditionKind1200))]
    //[KnownType(typeof(ConditionKind1205))]
    //[KnownType(typeof(ConditionKind1210))]
    //[KnownType(typeof(ConditionKind1215))]
    //[KnownType(typeof(ConditionKind1220))]
    //[KnownType(typeof(ConditionKind1225))]
    //[KnownType(typeof(ConditionKind1230))]
    //[KnownType(typeof(ConditionKind1235))]
    //[KnownType(typeof(ConditionKind1240))]
    //[KnownType(typeof(ConditionKind1241))]
    //[KnownType(typeof(ConditionKind1242))]
    //[KnownType(typeof(ConditionKind1243))]
    //[KnownType(typeof(ConditionKind1244))]
    //[KnownType(typeof(ConditionKind1245))]
    //[KnownType(typeof(ConditionKind1246))]
    //[KnownType(typeof(ConditionKind1247))]
    //[KnownType(typeof(ConditionKind1248))]
    //[KnownType(typeof(ConditionKind1249))]
    //[KnownType(typeof(ConditionKind1250))]
    //[KnownType(typeof(ConditionKind1255))]
    //[KnownType(typeof(ConditionKind1260))]
    //[KnownType(typeof(ConditionKind1265))]
    //[KnownType(typeof(ConditionKind1270))]
    //[KnownType(typeof(ConditionKind1275))]
    //[KnownType(typeof(ConditionKind1280))]
    //[KnownType(typeof(ConditionKind1285))]
    //[KnownType(typeof(ConditionKind1290))]
    //[KnownType(typeof(ConditionKind1291))]
    //[KnownType(typeof(ConditionKind1292))]
    //[KnownType(typeof(ConditionKind1293))]
    //[KnownType(typeof(ConditionKind1294))]
    //[KnownType(typeof(ConditionKind1295))]
    //[KnownType(typeof(ConditionKind1296))]
    //[KnownType(typeof(ConditionKind1297))]
    //[KnownType(typeof(ConditionKind1300))]
    //[KnownType(typeof(ConditionKind1310))]
    //[KnownType(typeof(ConditionKind1350))]
    //[KnownType(typeof(ConditionKind1360))]
    //[KnownType(typeof(ConditionKind1400))]
    //[KnownType(typeof(ConditionKind1401))]
    //[KnownType(typeof(ConditionKind1402))]
    //[KnownType(typeof(ConditionKind1403))]
    //[KnownType(typeof(ConditionKind1404))]
    //[KnownType(typeof(ConditionKind1410))]
    //[KnownType(typeof(ConditionKind1411))]
    //[KnownType(typeof(ConditionKind1412))]
    //[KnownType(typeof(ConditionKind1413))]
    //[KnownType(typeof(ConditionKind1414))]
    //[KnownType(typeof(ConditionKind1420))]
    //[KnownType(typeof(ConditionKind1421))]
    //[KnownType(typeof(ConditionKind1422))]
    //[KnownType(typeof(ConditionKind1423))]
    //[KnownType(typeof(ConditionKind1424))]
    //[KnownType(typeof(ConditionKind1425))]
    //[KnownType(typeof(ConditionKind1500))]
    //[KnownType(typeof(ConditionKind1510))]
    //[KnownType(typeof(ConditionKind1520))]
    //[KnownType(typeof(ConditionKind1530))]
    //[KnownType(typeof(ConditionKind1540))]
    //[KnownType(typeof(ConditionKind1550))]
    //[KnownType(typeof(ConditionKind1600))]
    //[KnownType(typeof(ConditionKind1601))]
    //[KnownType(typeof(ConditionKind1602))]
    //[KnownType(typeof(ConditionKind1603))]
    //[KnownType(typeof(ConditionKind1604))]
    //[KnownType(typeof(ConditionKind1605))]
    //[KnownType(typeof(ConditionKind1630))]
    //[KnownType(typeof(ConditionKind1635))]
    //[KnownType(typeof(ConditionKind1640))]
    //[KnownType(typeof(ConditionKind1645))]
    //[KnownType(typeof(ConditionKind1650))]
    //[KnownType(typeof(ConditionKind1655))]
    //[KnownType(typeof(ConditionKind1700))]
    //[KnownType(typeof(ConditionKind1705))]
    //[KnownType(typeof(ConditionKind1720))]
    //[KnownType(typeof(ConditionKind1725))]
    //[KnownType(typeof(ConditionKind1730))]
    //[KnownType(typeof(ConditionKind1735))]
    //[KnownType(typeof(ConditionKind1740))]
    //[KnownType(typeof(ConditionKind1745))]
    //[KnownType(typeof(ConditionKind1840))]
    //[KnownType(typeof(ConditionKind1845))]
    //[KnownType(typeof(ConditionKind2000))]
    //[KnownType(typeof(ConditionKind2005))]
    //[KnownType(typeof(ConditionKind2010))]
    //[KnownType(typeof(ConditionKind2015))]
    //[KnownType(typeof(ConditionKind2020))]
    //[KnownType(typeof(ConditionKind2025))]
    //[KnownType(typeof(ConditionKind2030))]
    //[KnownType(typeof(ConditionKind2035))]
    //[KnownType(typeof(ConditionKind2040))]
    //[KnownType(typeof(ConditionKind2045))]
    //[KnownType(typeof(ConditionKind2100))]
    //[KnownType(typeof(ConditionKind2105))]
    //[KnownType(typeof(ConditionKind2110))]
    //[KnownType(typeof(ConditionKind2115))]
    //[KnownType(typeof(ConditionKind2120))]
    //[KnownType(typeof(ConditionKind2125))]
    //[KnownType(typeof(ConditionKind2130))]
    //[KnownType(typeof(ConditionKind2135))]
    //[KnownType(typeof(ConditionKind2140))]
    //[KnownType(typeof(ConditionKind2145))]
    //[KnownType(typeof(ConditionKind2150))]
    //[KnownType(typeof(ConditionKind2155))]
    //[KnownType(typeof(ConditionKind2160))]
    //[KnownType(typeof(ConditionKind2165))]
    //[KnownType(typeof(ConditionKind2200))]
    //[KnownType(typeof(ConditionKind2205))]
    //[KnownType(typeof(ConditionKind2210))]
    //[KnownType(typeof(ConditionKind2215))]
    //[KnownType(typeof(ConditionKind2220))]
    //[KnownType(typeof(ConditionKind2225))]
    //[KnownType(typeof(ConditionKind2230))]
    //[KnownType(typeof(ConditionKind2235))]
    //[KnownType(typeof(ConditionKind2240))]
    //[KnownType(typeof(ConditionKind2245))]
    //[KnownType(typeof(ConditionKind2250))]
    //[KnownType(typeof(ConditionKind2255))]
    //[KnownType(typeof(ConditionKind2260))]
    //[KnownType(typeof(ConditionKind2265))]
    //[KnownType(typeof(ConditionKind2270))]
    //[KnownType(typeof(ConditionKind2275))]
    //[KnownType(typeof(ConditionKind2280))]
    //[KnownType(typeof(ConditionKind2285))]
    //[KnownType(typeof(ConditionKind2300))]
    //[KnownType(typeof(ConditionKind2305))]
    //[KnownType(typeof(ConditionKind2310))]
    //[KnownType(typeof(ConditionKind2315))]
    //[KnownType(typeof(ConditionKind2320))]
    //[KnownType(typeof(ConditionKind2325))]
    //[KnownType(typeof(ConditionKind2330))]
    //[KnownType(typeof(ConditionKind2335))]
    //[KnownType(typeof(ConditionKind2340))]
    //[KnownType(typeof(ConditionKind2345))]
    //[KnownType(typeof(ConditionKind2350))]
    //[KnownType(typeof(ConditionKind2355))]
    //[KnownType(typeof(ConditionKind2360))]
    //[KnownType(typeof(ConditionKind2365))]
    //[KnownType(typeof(ConditionKind2400))]
    //[KnownType(typeof(ConditionKind2405))]
    //[KnownType(typeof(ConditionKind2410))]
    //[KnownType(typeof(ConditionKind2415))]
    //[KnownType(typeof(ConditionKind2500))]
    //[KnownType(typeof(ConditionKind2501))]
    //[KnownType(typeof(ConditionKind2510))]
    //[KnownType(typeof(ConditionKind2515))]
    //[KnownType(typeof(ConditionKind2520))]
    //[KnownType(typeof(ConditionKind2525))]
    //[KnownType(typeof(ConditionKind2530))]
    //[KnownType(typeof(ConditionKind2535))]
    //[KnownType(typeof(ConditionKind2540))]
    //[KnownType(typeof(ConditionKind2545))]
    //[KnownType(typeof(ConditionKind2600))]
    //[KnownType(typeof(ConditionKind2601))]
    //[KnownType(typeof(ConditionKind2610))]
    //[KnownType(typeof(ConditionKind2620))]
    //[KnownType(typeof(ConditionKind2700))]
    //[KnownType(typeof(ConditionKind2705))]
    //[KnownType(typeof(ConditionKind2710))]
    //[KnownType(typeof(ConditionKind2711))]
    //[KnownType(typeof(ConditionKind2712))]
    //[KnownType(typeof(ConditionKind2713))]
    //[KnownType(typeof(ConditionKind2714))]
    //[KnownType(typeof(ConditionKind2715))]
    //[KnownType(typeof(ConditionKind2720))]
    //[KnownType(typeof(ConditionKind2725))]
    //[KnownType(typeof(ConditionKind2730))]
    //[KnownType(typeof(ConditionKind2735))]
    //[KnownType(typeof(ConditionKind2740))]
    //[KnownType(typeof(ConditionKind2745))]
    //[KnownType(typeof(ConditionKind2750))]
    //[KnownType(typeof(ConditionKind2755))]
    //[KnownType(typeof(ConditionKind2800))]
    //[KnownType(typeof(ConditionKind2805))]
    //[KnownType(typeof(ConditionKind2810))]
    //[KnownType(typeof(ConditionKind2815))]
    //[KnownType(typeof(ConditionKind2820))]
    //[KnownType(typeof(ConditionKind2825))]
    //[KnownType(typeof(ConditionKind2830))]
    //[KnownType(typeof(ConditionKind2835))]
    //[KnownType(typeof(ConditionKind2840))]
    //[KnownType(typeof(ConditionKind2845))]
    //[KnownType(typeof(ConditionKind2900))]
    //[KnownType(typeof(ConditionKind2905))]
    //[KnownType(typeof(ConditionKind3000))]
    //[KnownType(typeof(ConditionKind3005))]
    //[KnownType(typeof(ConditionKind3010))]
    //[KnownType(typeof(ConditionKind3015))]
    //[KnownType(typeof(ConditionKind3020))]
    //[KnownType(typeof(ConditionKind3025))]
    //[KnownType(typeof(ConditionKind3030))]
    //[KnownType(typeof(ConditionKind3035))]
    //[KnownType(typeof(ConditionKind3040))]
    //[KnownType(typeof(ConditionKind3045))]
    //[KnownType(typeof(ConditionKind3050))]
    //[KnownType(typeof(ConditionKind3051))]
    //[KnownType(typeof(ConditionKind3060))]
    //[KnownType(typeof(ConditionKind3065))]
    //[KnownType(typeof(ConditionKind3100))]
    //[KnownType(typeof(ConditionKind3105))]
    //[KnownType(typeof(ConditionKind3110))]
    //[KnownType(typeof(ConditionKind3115))]
    //[KnownType(typeof(ConditionKind3120))]
    //[KnownType(typeof(ConditionKind3125))]
    //[KnownType(typeof(ConditionKind3130))]
    //[KnownType(typeof(ConditionKind3135))]
    //[KnownType(typeof(ConditionKind3140))]
    //[KnownType(typeof(ConditionKind3145))]
    //[KnownType(typeof(ConditionKind3150))]
    //[KnownType(typeof(ConditionKind3155))]
    //[KnownType(typeof(ConditionKind3160))]
    //[KnownType(typeof(ConditionKind3165))]
    //[KnownType(typeof(ConditionKind3170))]
    //[KnownType(typeof(ConditionKind3175))]
    //[KnownType(typeof(ConditionKind3200))]
    //[KnownType(typeof(ConditionKind3205))]
    //[KnownType(typeof(ConditionKind3210))]
    //[KnownType(typeof(ConditionKind3215))]
    //[KnownType(typeof(ConditionKind4000))]
    //[KnownType(typeof(ConditionKind4005))]
    //[KnownType(typeof(ConditionKind4010))]
    //[KnownType(typeof(ConditionKind4015))]
    //[KnownType(typeof(ConditionKind4020))]
    //[KnownType(typeof(ConditionKind4025))]
    //[KnownType(typeof(ConditionKind4030))]
    //[KnownType(typeof(ConditionKind4035))]
    //[KnownType(typeof(ConditionKind4040))]
    //[KnownType(typeof(ConditionKind4045))]
    //[KnownType(typeof(ConditionKind4050))]
    //[KnownType(typeof(ConditionKind4055))]
    //[KnownType(typeof(ConditionKind4060))]
    //[KnownType(typeof(ConditionKind4065))]
    //[KnownType(typeof(ConditionKind4070))]
    //[KnownType(typeof(ConditionKind4075))]
    //[KnownType(typeof(ConditionKind4080))]
    //[KnownType(typeof(ConditionKind4085))]
    //[KnownType(typeof(ConditionKind4090))]
    //[KnownType(typeof(ConditionKind4095))]
    //[KnownType(typeof(ConditionKind4100))]
    //[KnownType(typeof(ConditionKind4105))]
    //[KnownType(typeof(ConditionKind4110))]
    //[KnownType(typeof(ConditionKind4115))]
    //[KnownType(typeof(ConditionKind4120))]
    //[KnownType(typeof(ConditionKind4125))]
    //[KnownType(typeof(ConditionKind4130))]
    //[KnownType(typeof(ConditionKind4135))]
    //[KnownType(typeof(ConditionKind4140))]
    //[KnownType(typeof(ConditionKind4145))]
    //[KnownType(typeof(ConditionKind4200))]
    //[KnownType(typeof(ConditionKind4205))]
    //[KnownType(typeof(ConditionKind4210))]
    //[KnownType(typeof(ConditionKind4215))]
    //[KnownType(typeof(ConditionKind4220))]
    //[KnownType(typeof(ConditionKind4225))]
    //[KnownType(typeof(ConditionKind5000))]
    //[KnownType(typeof(ConditionKind5005))]
    //[KnownType(typeof(ConditionKind5010))]
    //[KnownType(typeof(ConditionKind5015))]
    //[KnownType(typeof(ConditionKind5020))]
    //[KnownType(typeof(ConditionKind5025))]
    //[KnownType(typeof(ConditionKind5030))]
    //[KnownType(typeof(ConditionKind5035))]
    //[KnownType(typeof(ConditionKind5040))]
    //[KnownType(typeof(ConditionKind5045))]
    //[KnownType(typeof(ConditionKind5050))]
    //[KnownType(typeof(ConditionKind5055))]
    //[KnownType(typeof(ConditionKind5060))]
    //[KnownType(typeof(ConditionKind5065))]
    //[KnownType(typeof(ConditionKind5070))]
    //[KnownType(typeof(ConditionKind5075))]
    public class ConditionKind : GameObject  //abstract
    {
        protected static Person markedPerson = null;

        public virtual bool CheckConditionKind(Architecture architecture, Event e)
        {
            if (this.ID < 1000 || (this.ID >= 4000 && this.ID < 5000))
            {
                return architecture.Mayor != null && this.CheckConditionKind(architecture.Mayor, e);
            }
            if (this.ID >= 3000 && this.ID < 4000)
            {
                return architecture.BelongedFaction != null && this.CheckConditionKind(architecture.BelongedFaction, e);
            }
            return this.CheckConditionKind(architecture);
        }

        public virtual bool CheckConditionKind(Faction faction, Event e)
        {
            if (this.ID < 1000 || (this.ID >= 4000 && this.ID < 5000))
            {
                return faction.Leader != null && this.CheckConditionKind(faction.Leader, e);
            }
            return this.CheckConditionKind(faction);
        }

        public virtual bool CheckConditionKind(Person person, Event e)
        {
            if (this.ID >= 2000 && this.ID < 3000)
            {
                return person.LocationArchitecture != null && this.CheckConditionKind(person.LocationArchitecture, e);
            }
            if (this.ID >= 3000 && this.ID < 4000)
            {
                return person.BelongedFaction != null && this.CheckConditionKind(person.BelongedFaction, e);
            }
            return this.CheckConditionKind(person);
        }

        public virtual bool CheckConditionKind(Troop troop, Event e)
        {
            if (this.ID < 1000 || (this.ID >= 4000 && this.ID < 5000))
            {
                return troop.Leader != null && this.CheckConditionKind(troop.Leader, e);
            }
            if (this.ID >= 3000 && this.ID < 4000)
            {
                return troop.BelongedFaction != null && this.CheckConditionKind(troop.BelongedFaction, e);
            }
            return this.CheckConditionKind(troop);
        }

        public virtual bool CheckConditionKind(Architecture architecture)
        {
            if (this.ID < 1000 || (this.ID >= 4000 && this.ID < 5000))
            {
                return architecture.Mayor != null && this.CheckConditionKind(architecture.Mayor);
            }
            if (this.ID >= 3000 && this.ID < 4000)
            {
                return architecture.BelongedFaction != null && this.CheckConditionKind(architecture.BelongedFaction);
            }
            return false;
        }

        public virtual bool CheckConditionKind(Faction faction)
        {
            if (this.ID < 1000 || (this.ID >= 4000 && this.ID < 5000))
            {
                return faction.Leader != null && this.CheckConditionKind(faction.Leader);
            }
            return false;
        }

        public virtual bool CheckConditionKind(Person person)
        {
            if (this.ID >= 2000 && this.ID < 3000)
            {
                return person.LocationArchitecture != null && this.CheckConditionKind(person.LocationArchitecture);
            }
            if (this.ID >= 3000 && this.ID < 4000)
            {
                return person.BelongedFaction != null && this.CheckConditionKind(person.BelongedFaction);
            }
            return false;
        }

        public virtual bool CheckConditionKind(Troop troop)
        {
            if (this.ID >= 3000 && this.ID < 4000)
            {
                return troop.BelongedFaction != null && this.CheckConditionKind(troop.BelongedFaction);
            }
            return false;
        }

        public virtual void InitializeParameter(string parameter)
        {
        }

        public virtual void InitializeParameter2(string parameter)
        {
        }
    }
}

