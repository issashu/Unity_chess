using UnityEngine;

namespace Utils
{
    public struct MiscUtils
    {
        public static void shouldBeWaiting(float waitPeriod)
        {
            while ((waitPeriod -= Time.deltaTime) > 0)
            {
                Debug.Log($"AI snoozing for {waitPeriod} more seconds.");
            }
        }
    }
}