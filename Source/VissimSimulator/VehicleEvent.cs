﻿using System;
using System.Collections.Generic;
using System.Linq;
using VS = VissimSimulator;

namespace VissimSimulator
{
    public class VehicleEvent
    {
        #region private fields
        private Dictionary<Guid, Event> events = new Dictionary<Guid, Event>();
        #endregion //private fields

        #region public properties
        public string VehicleId { get; private set; }

        #endregion //public properties

        #region public methods
        public VehicleEvent(string vehicleId)
        {
            VehicleId = vehicleId;
        }


        /// <summary>
        /// Get an active event from this vehicle
        /// </summary>
        /// <param name="currentTicks">current time tick</param>
        /// <returns>Event</returns>
        public Event GetActiveEvent(long currentTicks)
        {
            return events.Values.Where(x => x.IsActive(currentTicks)).FirstOrDefault();
        }

        /// <summary>
        /// Get a future event from this vehicle
        /// </summary>
        /// <param name="currentTicks">current time tick</param>
        /// <returns>Event</returns>
        public Event GetFutureOnCallEvent(long currentTicks)
        {
            return events.Values.Where(x => x.TimeSpan.StartTick > currentTicks && x.EventType == EventType.OnCall).FirstOrDefault();
        }

        /// <summary>
        /// If this vehicle has OnCall event?
        /// </summary>
        /// <returns>True if it has, otherwise false</returns>
        public bool HasOnCallEvent()
        {
            return events.Values.Where(x => x.EventType == EventType.OnCall).Any();
        }

        /// <summary>
        /// Add a power-on event to this vehicle
        /// </summary>
        public void AddPowerOnEvent()
        {
            Event evet = new Event(EventType.PowerOn);
            events.Add(evet.guid, evet);
        }

        /// <summary>
        /// Add a on-call event to this vehicle
        /// </summary>
        /// <param name="currentTick">Current time tick</param>
        public void AddOnCallEvent(long currentTick)
        {
            Random rnd = new Random();
            //set the timespan range. start tick will always be current range to 
            long endTick = rnd.Next((int)currentTick + 60, 3600);
            Event evet = new Event(EventType.OnCall, new VS.TimeSpan(currentTick, endTick));
            events.Add(evet.guid, evet);
        }

        /// <summary>
        /// Remove an event from this vehicle
        /// </summary>
        /// <param name="evt">Event</param>
        public void removeEvent(Event evt)
        {
            events.Remove(evt.guid);
        }

        #endregion //public methods
    }
}
