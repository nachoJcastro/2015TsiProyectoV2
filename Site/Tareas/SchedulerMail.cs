using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Site.Tareas
{
    public class SchedulerMail
    {
        public static void Start()
        {
            /*IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailTarea>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(5)
                    .OnEveryDay()
                   .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(DateTime.Now.Hour, DateTime.Now.Minute))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);*/
        }
    }
}