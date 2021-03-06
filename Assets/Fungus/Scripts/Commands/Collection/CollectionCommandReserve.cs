// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Reserve space for given number of items in the collection
    /// </summary>
    [CommandInfo("Collection",
                 "Reserve",
                     "Reserve space for given number of items in the collection")]
    [AddComponentMenu("")]
    public class CollectionCommandReserve : CollectionBaseIntCommand
    {
        protected override void OnEnterInner()
        {
            collection.Value.Reserve(integer.Value);
        }
    }
}