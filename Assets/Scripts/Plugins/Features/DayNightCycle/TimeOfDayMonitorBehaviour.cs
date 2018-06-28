using System;
using Assets.Scripts.Plugins.Features.DayNightCycle.Api;
using ProjectXyz.Framework.Contracts;
using ProjectXyz.Plugins.Features.TimeOfDay;
using UnityEngine;

namespace Assets.Scripts.Plugins.Features.DayNightCycle
{
    public sealed class TimeOfDayMonitorBehaviour :
        MonoBehaviour,
        ITimeOfDayMonitorBehaviour
    {
        public IReadOnlyTimeOfDayManager TimeOfDayManager { get; set; }

        public TimeSpan UpdateDelay { get; set; }

        public Light LightSource { get; set; }

        public double MinRange { get; set; }

        public double MaxRange { get; set; }

        private float _triggerTime;

        private void Start()
        {
            Contract.RequiresNotNull(
                TimeOfDayManager,
                $"{nameof(TimeOfDayManager)} was not set on '{gameObject}.{this}'.");
            Contract.RequiresNotNull(
                LightSource,
                $"{nameof(LightSource)} was not set on '{gameObject}.{this}'.");
            Contract.Requires(
                UpdateDelay >= TimeSpan.Zero,
                $"{nameof(UpdateDelay)} was not set on '{gameObject}.{this}'.");
            Contract.Requires(
                MinRange >= 0,
                $"{nameof(MinRange)} was not set on '{gameObject}.{this}'.");
            Contract.Requires(
                MaxRange >= 0,
                $"{nameof(MaxRange)} was not set on '{gameObject}.{this}'.");
            Contract.Requires(
                MinRange <= MaxRange,
                $"{nameof(MinRange)} must be less than or equal to '{nameof(MaxRange)}' on '{gameObject}.{this}'.");
            ResetTriggerTime();
        }

        private void FixedUpdate()
        {
            if (Time.fixedTime < _triggerTime)
            {
                return;
            }

            ResetTriggerTime();

            var newRange = MinRange + Math.Sin(Math.PI - TimeOfDayManager.CyclePercent * Math.PI) * (MaxRange - MinRange);
            LightSource.range = (float)newRange;
        }

        private void ResetTriggerTime()
        {
            _triggerTime = Time.fixedTime + (float)UpdateDelay.TotalSeconds;
        }
    }
}