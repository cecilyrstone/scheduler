using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Scheduler.Services
{
    public class LocationService
    {
        public readonly TimeZone CurrentTimeZone = TimeZone.CurrentTimeZone;
        public readonly string CurrentCountryName = RegionInfo.CurrentRegion.DisplayName;

        public List<Appointment> AdjustAppointmentTimesForZone(List<Appointment> appointments)
        {
            foreach (var appointment in appointments)
            {
                // if dates are not UTC, this method call does nothing
                // First convert dates to UTC before storing in database
                CurrentTimeZone.ToLocalTime(appointment.Start);
                CurrentTimeZone.ToLocalTime(appointment.End);
            }

            return appointments;
        }

        public Appointment AdjustAppointmentTimeForZone(Appointment appointment)
        {
            CurrentTimeZone.ToLocalTime(appointment.Start);
            CurrentTimeZone.ToLocalTime(appointment.End);

            return appointment;
        }
    }
}
