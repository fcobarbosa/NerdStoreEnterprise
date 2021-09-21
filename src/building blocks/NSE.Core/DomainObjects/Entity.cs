using NSE.Core.Messages;
using System;
using System.Collections.Generic;

namespace NSE.Core.DomainObjects
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => this._notifications?.AsReadOnly();
        public void AddNotification(Event notification)
         {
            this._notifications = this._notifications ?? new List<Event>();
            this._notifications.Add(notification);
        }
        public void DeleteEvent(Event @event)
        {
            this._notifications?.Remove(@event);
        }
        public void ClearEvents()
        {
            this._notifications?.Clear();
        }


        #region Comparações
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;
            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }
        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }
        public static bool operator ==(Entity a, Entity b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }
        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }
        public override string ToString()
        {
            return $"{GetType().Name} [Id={Id}]";
        }
        #endregion
    }
}
