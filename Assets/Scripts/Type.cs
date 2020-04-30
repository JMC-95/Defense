using System;

namespace Type
{
    class Tower
    {
        public static int Archerlv1 = 0;
        public static int Archerlv2 = 1;
        public static int Archerlv3 = 2;
        public static int Archerlv4A = 3;
        public static int Archerlv4B = 4;

        public static int Canonlv1 = 5;
        public static int Canonlv2 = 6;
        public static int Canonlv3 = 7;
        public static int Canonlv4A = 8;
        public static int Canonlv4B = 9;

        public static int Magelv1 = 10;
        public static int Magelv2 = 11;
        public static int Magelv3 = 12;
        public static int Magelv4A = 13;
        public static int Magelv4B = 14;

        public static int Max = Magelv4B + 1;

        public static String GetTowerName(int towerType)
        {
            if(towerType == Archerlv1)
                return "ArcherTowerLV1";
            if (towerType == Archerlv2)
                return "ArcherTowerLV2";
            if (towerType == Archerlv3)
                return "ArcherTowerLV3";
            if (towerType == Archerlv4A)
                return "ArcherTowerLV4A";
            if (towerType == Archerlv4B)
                return "ArcherTowerLV4B";

            if (towerType == Canonlv1)
                return "CannonTowerLV1";
            if (towerType == Canonlv2)
                return "CannonTowerLV2";
            if (towerType == Canonlv3)
                return "CannonTowerLV3";
            if (towerType == Canonlv4A)
                return "CannonTowerLV4A";
            if (towerType == Canonlv4B)
                return "CannonTowerLV4B";

            if (towerType == Magelv1)
                return "MageTowerLV1";
            if (towerType == Magelv2)
                return "MageTowerLV2";
            if (towerType == Magelv3)
                return "MageTowerLV3";
            if (towerType == Magelv4A)
                return "MageTowerLV4A";
            if (towerType == Magelv4B)
                return "MageTowerLV4B";

            return null;
        }

        public static float GetBuildingDuration(int towerType)
        {
            if (towerType == Archerlv1)
                return 1.0f;
            if (towerType == Archerlv2)
                return 2.0f;
            if (towerType == Archerlv3)
                return 3.0f;
            if (towerType == Archerlv4A)
                return 4.0f;
            if (towerType == Archerlv4B)
                return 5.0f;

            if (towerType == Canonlv1)
                return 1.0f;
            if (towerType == Canonlv2)
                return 2.0f;
            if (towerType == Canonlv3)
                return 3.0f;
            if (towerType == Canonlv4A)
                return 4.0f;
            if (towerType == Canonlv4B)
                return 5.0f;

            if (towerType == Magelv1)
                return 1.0f;
            if (towerType == Magelv2)
                return 2.0f;
            if (towerType == Magelv3)
                return 3.0f;
            if (towerType == Magelv4A)
                return 4.0f;
            if (towerType == Magelv4B)
                return 5.0f;

            return 0.0f;
        }

        public static int GetBuildingPrice(int towerType)
        {
            if (towerType == Archerlv1)
                return 100;
            if (towerType == Archerlv2)
                return 200;
            if (towerType == Archerlv3)
                return 300;
            if (towerType == Archerlv4A)
                return 400;
            if (towerType == Archerlv4B)
                return 400;

            if (towerType == Canonlv1)
                return 100;
            if (towerType == Canonlv2)
                return 200;
            if (towerType == Canonlv3)
                return 300;
            if (towerType == Canonlv4A)
                return 400;
            if (towerType == Canonlv4B)
                return 400;

            if (towerType == Magelv1)
                return 100;
            if (towerType == Magelv2)
                return 200;
            if (towerType == Magelv3)
                return 300;
            if (towerType == Magelv4A)
                return 400;
            if (towerType == Magelv4B)
                return 400;

            return 0;
        }
    }

    class BuildingPointUiBotton
    {
        public static int Archer = 0;
        public static int Cannon = 1;
        public static int Mage = 2;
        public static int Max = Mage + 1;
    }

    class TowerUiBotton
    {
        public static int Upgrade = 0;
        public static int Sell = 1;
        public static int Max = Sell + 1;
    }

    class Line
    {
        public static int Left = 0;
        public static int Middle = 1;
        public static int Right = 2;
    }

    class Enemy
    {
        public static int Orc = 0;
        public static int Golem = 1;
        public static int Bat = 2;
        public static int Dragon = 3;
        public static int EvilMage = 4;
        public static int MonsterPlant = 5;
        public static int Skeleton = 6;
        public static int Slime = 7;
        public static int Spider = 8;
        public static int TurtleShell = 9;
        public static int Max = TurtleShell + 1;

        public static string ToString(int enemyType)
        {
            if (enemyType == 0)
                return "Orc";
            if (enemyType == 1)
                return "Golem";
            if (enemyType == 2)
                return "Bat";
            if (enemyType == 3)
                return "Dragon";
            if (enemyType == 4)
                return "EvilMage";
            if (enemyType == 5)
                return "MonsterPlant";
            if (enemyType == 6)
                return "Skeleton";
            if (enemyType == 7)
                return "Slime";
            if (enemyType == 8)
                return "Spider";
            if (enemyType == 9)
                return "TurtleShell";

            return null;
        }
    }



}
