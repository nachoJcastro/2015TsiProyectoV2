using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Tareas
{
    public class SchedulerSubasta
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<FinalizarTarea>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(1)
                    .OnEveryDay()
                     .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(DateTime.Now.Hour,DateTime.Now.Minute+1))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}