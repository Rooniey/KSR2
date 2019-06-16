using System;
using System.Collections.Generic;
using System.Linq;

namespace GUI.Base
{
    public class EventAggregator
    {

        private static Lazy<EventAggregator> _lazy = new Lazy<EventAggregator>(() => new EventAggregator());

        public static EventAggregator DefaultInstance => _lazy.Value;

        private List<IListen> subscribers = new List<IListen>();

        public EventAggregator()
        {

        }


        public void Subscribe(IListen model)
        {
            subscribers.Add(model);
        }

        public void Unsubscribe(IListen model)
        {
            subscribers.Remove(model);
        }

        public void Publish<T>(T message)
        {
            foreach (var item in subscribers.OfType<IListen<T>>())
            {
                item.Handle(message);
            }
        }
    }
}
