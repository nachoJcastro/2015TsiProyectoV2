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
            int minutos = 0;
            if (DateTime.Now.Minute==60) {
                 minutos = 1;
            }
            else { minutos = DateTime.Now.Minute + 1; }
                
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(1)
                    .OnEveryDay()
                     .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(DateTime.Now.Hour,minutos))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}