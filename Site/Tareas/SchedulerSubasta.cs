using log4net;
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
        //public static IScheduler scheduler;
       // public static ISchedulerFactory StdSchedulerFactory sfactor
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static void Start()
        {
            log.Warn("ENTRO  SchedulerSubasta START");
            
            MvcApplication.scheduler = StdSchedulerFactory.GetDefaultScheduler();
            MvcApplication.scheduler.Start();

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

            MvcApplication.scheduler.ScheduleJob(job, trigger);
            log.Warn("ENTRO  SchedulerSubasta START");
            //scheduler.ScheduleJob(job, trigger);
        }
    }
}