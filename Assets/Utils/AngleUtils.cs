using UnityEngine;

namespace Utils
{
    public class AngleUtils
    {
        public static float TranslateDegree(float min, float max, float val)
        {
            while (val < min)
            {
                val += 360f;
            }

            while (val > max)
            {
                val -= 360f;
            }

            return val;
        }

        public static float RoundToNearestRightAngle(float angle, float angleRange)
        {
            float[] possibleAngles = { -180f, -90f, 0f, 90f, 180f };

            angle = TranslateDegree(-180f, 180f, angle);

            foreach (float possibleAngle in possibleAngles)
            {
                float minAngle = Mathf.Max(possibleAngles[0], possibleAngle - angleRange);
                float maxAngle = Mathf.Min(possibleAngles[possibleAngles.Length - 1], possibleAngle + angleRange);

                if (angle >= minAngle && angle <= maxAngle)
                {
                    return possibleAngle;
                }
            }
            return -1f;
        }

    }

}