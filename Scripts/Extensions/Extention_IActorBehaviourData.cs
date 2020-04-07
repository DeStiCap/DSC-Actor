using System.Collections.Generic;
using DSC.Core;

namespace DSC.Actor
{
    public static class Extention_IActorBehaviourData
    {
        /// <summary>
        /// Remove this behaviour data type from list.
        /// </summary>
        /// <typeparam name="Data">Data type.</typeparam>
        /// <param name="listData">Actor behaviour data list.</param>
        /// /// <returns>Return true if found and remove data success.</returns>
        public static bool Remove<Data>(this List<IActorBehaviourData> listData) where Data : IActorBehaviourData
        {
            if (listData.TryGetData(out Data outData, out int outIndex))
            {
                listData.RemoveAt(outIndex);
                return true;
            }

            return false;
        }
    }
}