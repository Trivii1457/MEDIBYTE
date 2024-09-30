using Blazor.Infrastructure.Entities;
using Dominus.Backend.Application;
using Dominus.Backend.DataBase;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Blazor.BusinessLogic.Jobs
{

    #region JOB EXECUTION

    public class JobExecution
    {
        public static Dictionary<string, IScheduler> DicScheduler = new Dictionary<string, IScheduler>();
        public static List<JobData> Jobs { get; set; } = new List<JobData>();

        public async Task RunJobs()
        {
            //try
            //{
            //    List<DataBaseSetting> ListBD = DApp.DataBaseSettings.FindAll(x => x.TurnOnJobs == true);
            //    if (ListBD != null && ListBD.Count > 0) {
            //        foreach (DataBaseSetting BD in ListBD)
            //        {
            //            // Grab the Scheduler instance from the Factory
            //            NameValueCollection props = new NameValueCollection { { "quartz.serializer.type", "binary" } };
            //            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            //            IScheduler scheduler = await factory.GetScheduler();

            //            // and start it off
            //            await scheduler.Start();

            //            List<Job> jobs = new GenericBusinessLogic<Job>(BD).FindAll(x => x.Active);
            //            foreach (var job in jobs)
            //            {
            //                Type type = Type.GetType("Blazor.BusinessLogic.Jobs." + job.Class);
            //                if (type != null)
            //                {
            //                    List<JobDetail> jobDetails = new GenericBusinessLogic<JobDetail>(BD).FindAll(x => x.JobId == job.Id && x.Active);
            //                    foreach (var jobDetail in jobDetails)
            //                    {
            //                        IJob rutina = (IJob)Activator.CreateInstance(type);
            //                        JobData jobData = new JobData();
            //                        jobData.Class = job.Class;
            //                        jobData.CronExpression = jobDetail.CronExpression;
            //                        jobData.ConnectionName = BD.Name;

            //                        jobData.IJobDetail = JobBuilder.Create(type)
            //                        .WithIdentity(BD.Name + "_" + jobDetail.Id + "_" + jobData.Class, jobData.Class)
            //                        .UsingJobData("ConnectionName", BD.Name)
            //                        .Build();

            //                        jobData.ITrigger = TriggerBuilder.Create()
            //                            .WithIdentity(BD.Name + "_" + jobDetail.Id + "_" + jobData.Class, jobData.Class)
            //                            .WithCronSchedule(jobDetail.CronExpression)
            //                            .Build();

            //                        await scheduler.ScheduleJob(jobData.IJobDetail, jobData.ITrigger);
            //                        JobExecution.Jobs.Add(jobData);
            //                        await Console.Out.WriteLineAsync("Rutina " + jobData.Class + " creada - " + DateTime.Now.ToLongTimeString() + " -> " + job.Description);
            //                    }
            //                }
            //                else
            //                {
            //                    await Console.Out.WriteLineAsync(" ::::::::::: Clase (" + job.Class + ") no existe o esta mal ubicado en el proyecto ::::::::::: ");
            //                }

            //            }
            //            DicScheduler.Add(BD.Name, scheduler);
            //        }
            //    }

            //}
            //catch (SchedulerException se)
            //{
            //    Console.WriteLine(se.Message);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
    }

    public class JobData
    {
        public IJobDetail IJobDetail { get; set; }
        public ITrigger ITrigger { get; set; }
        public string Class { get; set; }
        public string CronExpression { get; set; }
        public string ConnectionName { get; set; }
    }

    public class ConsoleLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                }
                return true;
            };
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
