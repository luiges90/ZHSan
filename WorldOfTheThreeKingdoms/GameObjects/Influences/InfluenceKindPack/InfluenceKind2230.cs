using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2230 : InfluenceKind
    {
        private int type = 0;

        public override void ApplyInfluenceKind(Faction faction)
        {
            switch (this.type)
            {
                case 0:
                    faction.AllowAttackAfterMoveOfBubing = true;
                    break;

                case 1:
                    faction.AllowAttackAfterMoveOfNubing = true;
                    break;

                case 2:
                    faction.AllowAttackAfterMoveOfQibing = true;
                    break;

                case 3:
                    faction.AllowAttackAfterMoveOfShuijun = true;
                    break;

                case 4:
                    faction.AllowAttackAfterMoveOfQixie = true;
                    break;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.type = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Faction faction)
        {
            switch (this.type)
            {
                case 0:
                    faction.AllowAttackAfterMoveOfBubing = false;
                    break;

                case 1:
                    faction.AllowAttackAfterMoveOfNubing = false;
                    break;

                case 2:
                    faction.AllowAttackAfterMoveOfQibing = false;
                    break;

                case 3:
                    faction.AllowAttackAfterMoveOfShuijun = false;
                    break;

                case 4:
                    faction.AllowAttackAfterMoveOfQixie = false;
                    break;
            }
        }
    }
}

