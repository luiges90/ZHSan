using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Serilog;

namespace GameObjects.Conditions
{
    [DataContract]
    public class Condition : GameObject
    {
        # region DataMember

        /// <summary>
        /// 条件类型
        /// </summary>
        [DataMember]
        public ConditionKind Kind { get; set; }

        private string parameter;
        private int? intParameter;
        private float? floatParameter;


        /// <summary>
        /// 参数1
        /// </summary>
        [DataMember]
        public string Parameter
        {
            get => parameter;
            set
            {
                parameter = value;
                intParameter = null; // 重置缓存
                floatParameter = null;
            }
        }

        private string parameter2;
        private int? intParameter2;
        private float? floatParameter2;

        /// <summary>
        /// 参数2
        /// </summary>
        [DataMember]
        public string Parameter2
        {
            get => parameter2;
            set
            {
                parameter2 = value;
                intParameter2 = null; // 重置缓存
                floatParameter2 = null;
            }
        }

        #endregion

        /// <summary>
        /// 获取参数1解析的int值
        /// </summary>
        /// <returns></returns>
        public int GetIntParam()
        {
            if (!intParameter.HasValue)
            {
                intParameter = int.TryParse(parameter, out int v) ? v : 0;
            }
            return intParameter.Value;
        }

        /// <summary>
        /// 获取参数1解析的float值
        /// </summary>
        /// <returns></returns>
        public float GetFloatParam()
        {
            if (!floatParameter.HasValue)
            {
                floatParameter = float.TryParse(parameter, out float v) ? v : 0;
            }
            return floatParameter.Value;
        }

        /// <summary>
        /// 获取参数2解析的int值
        /// </summary>
        /// <returns></returns>
        public int GetIntParam2()
        {
            if (!intParameter2.HasValue)
            {
                intParameter2 = int.TryParse(parameter2, out int v) ? v : 0;
            }
            return intParameter2.Value;
        }

        /// <summary>
        /// 获取参数2解析的float值
        /// </summary>
        /// <returns></returns>
        public float GetFloatParam2()
        {
            if (!floatParameter2.HasValue)
            {
                floatParameter2 = float.TryParse(parameter2, out float v) ? v : 0;
            }
            return floatParameter2.Value;
        }

        private static readonly ILogger logger = Log.ForContext<Condition>();

        /// <summary>
        /// 校验条件列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="conditions"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool CheckConditionList<T>(IEnumerable<Condition> conditions, T target) where T : GameObject
        {
            if (target == null) return false;

            if (conditions == null) return true;
            
            bool result = true;
            bool negate = false;

            foreach (var condition in conditions)
            {
                var conditionKind = condition.Kind;

                // 忽略不存在的条件类型，并重置negate
                if (conditionKind == null)
                {
                    logger.Error($"条件Id:[{condition.ID}]不存在种类");
                    negate = false;
                    result = false;
                    continue;
                }

                if (conditionKind.ID == ConditionKindIds.Negate)
                {
                    negate = true;
                }
                else if (conditionKind.ID == ConditionKindIds.Or)
                {
                    // 记录or前有negate，并重置negate
                    if (negate)
                    {
                        logger.Error("或前设置取反，已忽略取反操作");
                        negate = false;
                    }

                    if (result) return true;
                    result = true;
                }
                else
                {
                    try
                    {
                        bool passed = condition.CheckCondition(target) ^ negate; // negate时取反
                        if (!passed) result = false;

                        negate = false;
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }
                }
            }

            if (negate)
            {
                logger.Error("末尾的取反操作无效");
            }

            return result;
        }

        public bool CheckCondition<T>(T target) where T : GameObject
        {
            return target switch
            {
                Faction faction => Kind.CheckConditionKind(this, faction),
                Architecture arch => Kind.CheckConditionKind(this, arch),
                Person person => Kind.CheckConditionKind(this, person),
                Troop troop => Kind.CheckConditionKind(this, troop),
                _ => throw new NotSupportedException($"不支持的条件对象: {typeof(T)}")
            };
        }

        /// <summary>
        /// 加载条件权重
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="weightStr"></param>
        /// <returns></returns>
        public static Dictionary<Condition, float> LoadConditionWeightFromString(Dictionary<int, Condition> conditions, string weightStr)
        {
            var weights = weightStr.Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var result = new Dictionary<Condition, float>();

            var length = weights.Length;
            for (int i = 0; i < length && i + 1 < length; i += 2)
            {
                var conditionIdStr = weights[i];
                var valueStr = weights[i + 1];

                if (!int.TryParse(conditionIdStr, out var conditionId))
                {
                    logger.Error($"无法解析条件Id:{conditionIdStr}");
                }

                if (!float.TryParse(valueStr, out var value))
                {
                    logger.Error($"无法解析权重值:{valueStr}");
                }

                if (!conditions.TryGetValue(conditionId, out var condition))
                {
                    logger.Error($"条件Id:[{conditionIdStr}]不存在");
                }
                
                result.Add(condition, value);
            }

            return result;
        }

        public static bool CheckPersonalityCondition(ICollection<Condition> conditions, Person person)
        {
            foreach (var condition in conditions)
            {
                var kindId = condition.Kind.ID;

                if (kindId == 600 || kindId == 610) //check personality kind only
                {
                    if (!condition.CheckCondition(person)) return false;
                }
            }

            return true;
        }
    }
}