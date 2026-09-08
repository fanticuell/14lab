using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacesLibrary
{
    public class CollectionHandlerEventArgs
    {
        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public object ChangedItem { get; set; }

        public CollectionHandlerEventArgs(string collectionName, string changeType, object changedItem)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ChangedItem = changedItem;
        }

        public override string ToString()
        {
            return $"Коллекция: '{CollectionName}' | Действие: {ChangeType} | Объект: {ChangedItem?.ToString() ?? "null"}";
        }
    }
}
